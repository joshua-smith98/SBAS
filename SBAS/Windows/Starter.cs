using System;
using System.Windows.Forms;

namespace SBAS
{
    public partial class Starter : Form
    {
        public Starter()
        {
            InitializeComponent();
        }

        private void PlayerButton_Click(object sender, EventArgs e)
        {
            SBAS.MainWindowController.MainPlayer.Show();
            Close();
        }

        private void ScripterButton_Click(object sender, EventArgs e)
        {
            SBAS.MainWindowController.MainScripter.Show();
            Close();
        }

        private void EditorButton_Click(object sender, EventArgs e)
        {
            SBAS.MainWindowController.MainEditor.Show();
            Close();
        }
    }
}
