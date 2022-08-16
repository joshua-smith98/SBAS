using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace SBAS
{
    public partial class Previewer : Form
    {
        Project.Sentence MainSentence;

        public Previewer()
        {
            InitializeComponent();
        }

        private void PlayButton_Click(object sender, EventArgs e)
        {
            MainSentence.Play();
        }

        private void TextBox_TextChanged(object sender, EventArgs e)
        {
            MainSentence = new Project.Sentence(TextBox.Text, SBAS.MainController.CurrentProject);
            PlayButton.Enabled = MainSentence.Valid;
            textBox1.Text = String.Join(" ", MainSentence.Strings.Select(x => x.Text));
        }

        private void Previewer_Shown(object sender, EventArgs e)
        {
            if (SBAS.MainController.CurrentProject == null) Close();
        }
    }
}
