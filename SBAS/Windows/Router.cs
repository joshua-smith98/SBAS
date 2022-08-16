using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace SBAS
{
    public partial class Player : Form
    {
        bool Refreshing = false;

        public Player()
        {
            InitializeComponent();
            RefreshAll();
        }

        private void newProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog()
            {
                Filter = "SBAS Projects (*.sbp)|*.sbp",
                OverwritePrompt = true,
                Title = "Create New SBAS Project...",
                CheckFileExists = false,
                InitialDirectory = Directory.GetCurrentDirectory(),
                ValidateNames = true,
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                SBAS.MainController.NewProject(saveFileDialog.FileName, Path.GetFileNameWithoutExtension(saveFileDialog.FileName));
                RefreshAll();
            }
        }

        private void openProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                Filter = "SBAS Projects (*.sbp)|*.sbp",
                CheckFileExists = true,
                Multiselect = false,
                Title = "Open SBAS Project File...",
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                SBAS.MainController.OpenProject(openFileDialog.FileName);
                RefreshAll();
            }
        }

        private void saveProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SBAS.MainController.SaveProject();
        }

        private void saveProjectAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog()
            {
                Filter = "SBAS Projects (*.sbp)|*.sbp",
                OverwritePrompt = true,
                Title = "Save SBAS Project As...",
                CheckFileExists = false,
                InitialDirectory = SBAS.MainController.CurrentProject.fileStream.Name,
                ValidateNames = true,
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                SBAS.MainController.SaveProjectAs(saveFileDialog.FileName);
                RefreshAll();
            }
        }

        private void closeProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Save changes to current project?", "Save Changes?", MessageBoxButtons.YesNoCancel);

            if (result == DialogResult.Yes) SBAS.MainController.SaveProject();

            if (result != DialogResult.Cancel)
            {
                SBAS.MainController.CloseProject();
                SBAS.MainController.ProjectClosed.WaitOne();
                RefreshAll();
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void exitToStarterToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void Editor_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("Save changes to current project?", "Save Changes?", MessageBoxButtons.YesNoCancel);

            if (result == DialogResult.Yes) SBAS.MainController.SaveProject();

            if (result != DialogResult.Cancel)
            {
                Application.Exit();
            }
            else
            {
                e.Cancel = true;
            }
        }

        #region Refresh Functions

        void RefreshAll()
        {
            if (SBAS.MainController.CurrentProject != null)
            {
                ClearAll();
                tabControl1.Enabled = true;

                Text = SBAS.MainController.CurrentProject.Name;

                RefreshLineList();
            }
            else
            {
                ClearAll();
                tabControl1.Enabled = false;
            }
        }

        void RefreshLineList()
        {
            Refreshing = true;

            if (SBAS.MainController.CurrentProject.Lines.Count > 0)
            {
                object SelectedLine = LineList.SelectedItem;
                LineList.Items.Synchronise(SBAS.MainController.CurrentProject.Lines);
                LineList.Sorted = true;
                LineList.SelectedItem = SelectedLine;
            }
            else
            {
                LineList.Items.Clear();
                BranchFrom.Items.Clear();
                BranchFrom.Text = String.Empty;
                ConnectTo.Items.Clear();
                ConnectTo.Text = String.Empty;
            }

            Refreshing = false;
            RefreshLineProperties();
            RefreshStopList();
        }

        void RefreshLineProperties()
        {
            Refreshing = true;

            if (LineList.SelectedItem != null)
            {
                Project.Line SelectedLine = (Project.Line)LineList.SelectedItem;
                LineName.Text = SelectedLine.Name;

                BranchFrom.Items.Clear();
                BranchFrom.Text = String.Empty;
                BranchFrom.Items.Add("(none)");
                BranchFrom.Items.AddRange(SBAS.MainController.CurrentProject.Lines.ToArray());
                BranchFrom.Items.Remove(SelectedLine);
                BranchFrom.Sorted = true;
                ConnectTo.Items.Clear();
                ConnectTo.Text = String.Empty;
                ConnectTo.Items.Add("(none)");
                ConnectTo.Items.AddRange(SBAS.MainController.CurrentProject.Lines.ToArray());
                ConnectTo.Items.Remove(SelectedLine);
                ConnectTo.Sorted = true;

                if (SelectedLine.BranchesFrom == null)
                {
                    BranchFrom.SelectedItem = "(none)";
                    BranchAt.Items.Clear();
                    BranchAt.Text = String.Empty;
                    BranchAt.Enabled = false;
                }
                else
                {
                    BranchFrom.SelectedItem = SelectedLine.BranchesFrom;
                    BranchAt.Items.Clear();
                    BranchAt.Items.AddRange(SelectedLine.BranchesFrom.Stops.ToArray());
                    BranchAt.Enabled = true;
                    BranchAt.SelectedItem = SelectedLine.BranchesAt;
                }

                if (SelectedLine.ConnectsTo == null)
                {
                    ConnectTo.SelectedItem = "(none)";
                    ConnectAt.Items.Clear();
                    ConnectAt.Text = String.Empty;
                    ConnectAt.Enabled = false;
                }
                else
                {
                    ConnectTo.SelectedItem = SelectedLine.ConnectsTo;
                    ConnectAt.Items.Clear();
                    ConnectAt.Items.AddRange(SelectedLine.ConnectsTo.Stops.ToArray());
                    ConnectAt.Enabled = true;
                    ConnectAt.SelectedItem = SelectedLine.ConnectsAt;
                }
                DeleteLine.Enabled = true;
                LinePropGroup.Enabled = true;
            }
            else
            {
                LineName.Text = String.Empty;
                BranchFrom.Text = String.Empty;
                BranchAt.Items.Clear();
                ConnectTo.Text = String.Empty;
                ConnectAt.Items.Clear();
                LinePropGroup.Enabled = false;
                DeleteLine.Enabled = false;
            }

            NewLine.Enabled = true;

            Refreshing = false;
        }

        void RefreshStopList()
        {
            Refreshing = true;

            if (LineList.SelectedItem != null)
            {
                Project.Line SelectedLine = (Project.Line)LineList.SelectedItem;

                if (SelectedLine.Stops.Count > 0)
                {
                    object SelectedStop = StopList.SelectedItem;

                    List<object> Items = new List<object>();
                    Project.Line[] Branches = SelectedLine.GetBranches();

                    foreach (Project.Line.Stop stop in SelectedLine.Stops)
                    {
                        if (stop == SelectedLine.Stops.First() && SelectedLine.BranchesFrom != null) Items.Add(String.Format("[To {0} Line]", SelectedLine.BranchesFrom.Name));

                        Project.Line[] ConnectingLine = Branches.Where(x => x.ConnectsTo == SelectedLine && SelectedLine.Stops.FindIndex(y => y == x.ConnectsAt) == SelectedLine.Stops.FindIndex(y => y == stop)).ToArray();
                        if (ConnectingLine != null)
                        {
                            foreach (Project.Line line in ConnectingLine) Items.Add(String.Format("---> [To {0} Line]", line.Name));
                        }

                        Items.Add(stop);

                        Project.Line[] BranchingLine = Branches.Where(x => x.BranchesFrom == SelectedLine && SelectedLine.Stops.FindIndex(y => y == x.BranchesAt) == SelectedLine.Stops.FindIndex(y => y == stop)).ToArray();
                        if (BranchingLine != null)
                        {
                            foreach (Project.Line line in BranchingLine) Items.Add(String.Format("---> [To {0} Line]", line.Name));
                        }

                        if (stop == SelectedLine.Stops.Last() && SelectedLine.ConnectsTo != null) Items.Add(String.Format("[To {0} Line]", SelectedLine.ConnectsTo.Name));
                    }
                    StopList.Items.Clear();
                    StopList.Items.Synchronise(Items);
                    StopList.SelectedItem = SelectedStop;
                }
                else
                {
                    StopList.Items.Clear();
                }
                StopsGroup.Enabled = true;
            }
            else
            {
                StopList.Items.Clear();
                StopsGroup.Enabled = false;
            }

            Refreshing = false;
            RefreshStopProperties();
        }

        void RefreshStopProperties()
        {
            Refreshing = true;

            if (StopList.SelectedItem != null)
            {
                Project.Line.Stop SelectedStop = (Project.Line.Stop)StopList.SelectedItem;

                StopName.Items.Clear();
                StopName.Items.AddRange(SBAS.MainController.CurrentProject.Strings.Except(SBAS.MainController.CurrentProject.Stops.Select(x => x.Name)).ToArray());
                StopName.Items.Add(SelectedStop.Name);
                StopName.Sorted = true;
                StopName.SelectedItem = SelectedStop.Name;
                StopPropGroup.Enabled = true;
                DeleteStop.Enabled = true;
            }
            else
            {
                StopName.Items.Clear();
                StopPropGroup.Enabled = false;
                DeleteStop.Enabled = false;
            }

            NewStop.Enabled = true;

            Refreshing = false;
        }

        void ClearAll()
        {
            Refreshing = true;
            LineList.Items.Clear();

            LineName.Text = String.Empty;
            BranchFrom.Items.Clear();
            BranchFrom.Text = String.Empty;
            BranchAt.Items.Clear();
            BranchAt.Text = String.Empty;
            ConnectTo.Items.Clear();
            ConnectTo.Text = String.Empty;
            ConnectAt.Items.Clear();
            ConnectAt.Text = String.Empty;

            StopList.Items.Clear();
            StopName.Text = String.Empty;

            Refreshing = false;
        }

        #endregion

        private void LineList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!Refreshing)
            {
                if (LineList.SelectedItem != null && LineList.SelectedItem.GetType() != typeof(Project.Line))
                {
                    Refreshing = true;
                    LineList.SelectedItem = null;
                    Refreshing = false;
                }
                else
                {
                    RefreshLineProperties();
                    RefreshStopList();
                }
            }
        }

        private void NewLine_Click(object sender, EventArgs e)
        {
            string[] DisallowedNames = SBAS.MainController.CurrentProject.Lines.Select(x => x.Name).ToArray();
            Namer nameDialog = new Namer(DisallowedNames, SBAS.MainController.CurrentProject.Strings.Select(x => x.ID).Except(DisallowedNames).ToArray());
            nameDialog.ShowDialog();

            if (nameDialog.DialogResult == DialogResult.OK)
            {
                Project.Line NewLine = new Project.Line(nameDialog.NameResult, SBAS.MainController.CurrentProject);
                SBAS.MainController.CurrentProject.Lines.Add(NewLine);
            }

            RefreshLineList();
        }

        private void DeleteLine_Click(object sender, EventArgs e)
        {
            Project.Line SelectedLine = (Project.Line)LineList.SelectedItem;

            if (MessageBox.Show(String.Format("Are you sure you want to delete line: '{0}'? This is permanent!", SelectedLine.Name), "Delete Line", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                SBAS.MainController.CurrentProject.Lines.Remove(SelectedLine);
            }

            RefreshLineList();
        }

        private void LineName_TextChanged(object sender, EventArgs e)
        {
            if (!Refreshing)
            {
                Project.Line SelectedLine = (Project.Line)LineList.SelectedItem;
                SelectedLine.Name = LineName.Text;
            }
        }

        private void BranchFrom_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Refreshing) return;

            Project.Line SelectedLine = (Project.Line)LineList.SelectedItem;

            if (BranchFrom.Text == "(none)")
            {
                SelectedLine.BranchesFrom = null;
            }
            else
            {
                SelectedLine.BranchesFrom = (Project.Line)BranchFrom.SelectedItem;
                SelectedLine.BranchesAt = SelectedLine.BranchesFrom.Stops.LastOrDefault();
            }

            RefreshLineList();
        }

        private void BranchAt_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Refreshing) return;

            Project.Line SelectedLine = (Project.Line)LineList.SelectedItem;

            SelectedLine.BranchesAt = (Project.Line.Stop)BranchAt.SelectedItem;

            RefreshLineList();
        }

        private void ConnectTo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Refreshing) return;

            Project.Line SelectedLine = (Project.Line)LineList.SelectedItem;

            if (ConnectTo.Text == "(none)")
            {
                SelectedLine.ConnectsTo = null;
            }
            else
            {
                SelectedLine.ConnectsTo = (Project.Line)ConnectTo.SelectedItem;
                SelectedLine.ConnectsAt = SelectedLine.ConnectsTo.Stops.FirstOrDefault();
            }

            RefreshLineList();
        }

        private void ConnectAt_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Refreshing) return;

            Project.Line SelectedLine = (Project.Line)LineList.SelectedItem;

            SelectedLine.ConnectsAt = (Project.Line.Stop)ConnectAt.SelectedItem;

            RefreshLineList();
        }

        private void StopsGroup_Enter(object sender, EventArgs e)
        {

        }

        private void StopList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!Refreshing)
            {
                if (StopList.SelectedItem.GetType() != typeof(Project.Line.Stop))
                {
                    Refreshing = true;
                    StopList.SelectedItem = null;
                    Refreshing = false;
                }
                else
                {
                    RefreshStopProperties();
                }
            }
        }

        private void NewStop_Click(object sender, EventArgs e)
        {
            Project.Line SelectedLine = (Project.Line)LineList.SelectedItem;
            string[] DisallowedNames = SBAS.MainController.CurrentProject.Stops.Select(x => x.Name.ID).ToArray();
            ListNamer nameDialog = new ListNamer(SBAS.MainController.CurrentProject.Strings.Select(x => x.ID).Except(DisallowedNames).ToArray(), DisallowedNames, false);
            nameDialog.ShowDialog();

            if (nameDialog.DialogResult == DialogResult.OK)
            {
                Project.Line.Stop NewStop = new Project.Line.Stop(SBAS.MainController.CurrentProject.Strings.Find(x => x.ID == nameDialog.NameResult), SBAS.MainController.CurrentProject);
                SelectedLine.Stops.Add(NewStop);
            }

            RefreshLineList();
        }

        private void DeleteStop_Click(object sender, EventArgs e)
        {
            Project.Line SelectedLine = (Project.Line)LineList.SelectedItem;
            Project.Line.Stop SelectedStop = (Project.Line.Stop)StopList.SelectedItem;
            SelectedLine.Stops.Remove(SelectedStop);

            RefreshLineList();
        }

        private void StopName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!Refreshing)
            {
                Project.Line.Stop SelectedStop = (Project.Line.Stop)StopList.SelectedItem;
                Project.String SelectedString = (Project.String)StopName.SelectedItem;
                SelectedStop.Name = SelectedString;
                RefreshLineList();
            }
        }

        private void StopMoveUpButton_Click(object sender, EventArgs e)
        {
            Project.Line SelectedLine = (Project.Line)LineList.SelectedItem;
            Project.Line.Stop SelectedStop = (Project.Line.Stop)StopList.SelectedItem;

            SelectedLine.MoveStopUp(SelectedStop);
            RefreshStopList();
        }

        private void StopMoveDownButton_Click(object sender, EventArgs e)
        {
            Project.Line SelectedLine = (Project.Line)LineList.SelectedItem;
            Project.Line.Stop SelectedStop = (Project.Line.Stop)StopList.SelectedItem;

            SelectedLine.MoveStopDown(SelectedStop);
            RefreshStopList();
        }

        private void StopName_TextUpdate(object sender, EventArgs e)
        {

        }

        private void StopName_Validating(object sender, CancelEventArgs e)
        {
            if (Refreshing) return;

            if (StopName.Items.Cast<Project.String>().Select(x => x.ToString()).Contains(StopName.Text))
            {
                StopName.SelectedItem = StopName.Items.Cast<object>().ToList().Find(x => ((Project.String)x).ToString() == StopName.Text);
            }
            else StopName.SelectedItem = ((Project.Line.Stop)StopList.SelectedItem).Name;
        }

        private void LineName_Validated(object sender, EventArgs e)
        {
            if (Refreshing) return;

            RefreshLineList();
            LineList.RefreshSelected();
        }

        private void ReverseStops_Click(object sender, EventArgs e)
        {
            Project.Line SelectedLine = (Project.Line)LineList.SelectedItem;

            SelectedLine.Stops.Reverse();

            RefreshStopList();
        }
    }
}
