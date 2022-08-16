using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace SBAS
{
    public partial class ListNamer : Form
    {
        public string NameResult = String.Empty;
        string[] NotAllowedList;
        bool AllowNonListItems;

        public ListNamer(string[] OptionList, string[] NotAllowedList = null, bool AllowNonListItems = true)
        {
            InitializeComponent();

            if (NotAllowedList != null)
            {
                this.NotAllowedList = NotAllowedList;
            }
            else
            {
                this.NotAllowedList = new List<string>().ToArray();
            }

            this.AllowNonListItems = AllowNonListItems;
            NameBox.Items.AddRange(OptionList.Where(x => !this.NotAllowedList.Contains(x)).ToArray());
        }

        private void Namer_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && NameResult != String.Empty && !NotAllowedList.Contains(NameResult))
            {
                DialogResult = DialogResult.OK;
                Close();
            }

            if (e.KeyCode == Keys.Escape)
            {
                DialogResult = DialogResult.Cancel;
                Close();
            }
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void NameBox_TextChanged(object sender, EventArgs e)
        {
            NameResult = NameBox.Text;
            if (NameResult != String.Empty && !NotAllowedList.Contains(NameResult) && (AllowNonListItems || NameBox.Items.Contains(NameBox.Text)))
            {
                OKButton.Enabled = true;
            }
            else
            {
                OKButton.Enabled = false;
            }
        }
    }
}
