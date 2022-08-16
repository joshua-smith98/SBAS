using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace SBAS
{
    public partial class Namer : Form
    {
        public string NameResult = String.Empty;
        string[] NotAllowedList;

        public Namer(string[] NotAllowedList = null, string[] SuggestionList = null)
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

            if (SuggestionList != null)
            {
                NameBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                NameBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
                NameBox.AutoCompleteCustomSource.AddRange(SuggestionList);
            }
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
            if (NameResult != String.Empty && !NotAllowedList.Contains(NameResult))
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
