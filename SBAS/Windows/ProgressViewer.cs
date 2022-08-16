using System;
using System.Windows.Forms;

namespace SBAS
{
    public partial class ProgressViewer : Form
    {
        public ProgressViewer()
        {
            InitializeComponent();
        }

        public void Initialise(string Title, int Max)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => { Initialise(Title, Max); }));
                return;
            }

            Text = Title;
            TaskLabel.Text = Title;
            ProgressBar.Value = 0;
            ProgressBar.Maximum = Max;
        }

        public void Update(int Value)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => { Update(Value); }));
                return;
            }

            ProgressBar.Value = Value;
            TaskLabel.Text = Text;
        }

        public void Update(int Value, string Task)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => { Update(Value, Task); }));
                return;
            }

            ProgressBar.Value = Value;
            TaskLabel.Text = Task;
        }
    }
}
