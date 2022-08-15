using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using NAudio;
using NAudio.Utils;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using NAudio.FileFormats;
using NAudio.MediaFoundation;

namespace SBAS
{
    public static class SBAS
    {
        public static Controller MainController;
        public static WindowController MainWindowController;
        public static Thread ControllerThread = new Thread(StartWindowController);
        public static Thread WindowControllerThread = new Thread(StartController);
        public static Thread ConsoleControllerThread = new Thread(StartConsoleController);
        static string NullAudioFileDir = "Null.wav";
        public static Project.AudioFile NullAudioFile;
        static Random rand = new Random();
        public static string TempDir
        {
            get
            {
                while (true)
                {
                    string temp = Path.Combine(Directory.GetCurrentDirectory(), "TEMP", rand.Next().ToString());
                    if (!File.Exists(temp)) return temp;
                }

            }
        }

        static void Main(string[] args)
        {
            if (!Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), "TEMP"))) Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "TEMP"));
            if (!File.Exists(NullAudioFileDir))
            {
                float[] wavnoise = new float[8000];
                for (int i = 0; i < wavnoise.Length; i++) wavnoise[i] = (float)rand.Next(int.MinValue, int.MaxValue);
                byte[] wavnoisebyte = wavnoise.SelectMany(x => BitConverter.GetBytes(x)).ToArray();
                WaveFormat waveFormat = new WaveFormat(16000, 8, 1);
                MemoryStream noise = new MemoryStream(wavnoisebyte);
                RawSourceWaveStream noiseraw = new RawSourceWaveStream(noise, waveFormat);
                WaveFileWriter.CreateWaveFile(NullAudioFileDir, noiseraw);
            }
            NullAudioFile = new Project.AudioFile(NullAudioFileDir, null);
            Application.EnableVisualStyles();
            ControllerThread.SetApartmentState(ApartmentState.STA);
            WindowControllerThread.SetApartmentState(ApartmentState.STA);
            ConsoleControllerThread.SetApartmentState(ApartmentState.STA);
            ControllerThread.Start();

            if (args.Length == 0)
            {
                WindowControllerThread.Start();
            }

        }

        static void StartWindowController()
        {
            MainWindowController = new WindowController();
            MainWindowController.MainStarter.Show();
            Application.Run();
        }

        static void StartController()
        {
            MainController = new Controller();
            Application.Run();
        }

        static void StartConsoleController()
        {

        }
    }

    public class Controller : Control
    {
        public Project CurrentProject;
        public string CurrentTask;
        public int CurrentPercent;

        public AutoResetEvent ProjectClosed = new AutoResetEvent(false);

        public void NewProject(string Dir, string Name)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => NewProject(Dir, Name)));
                return;
            }

            CurrentProject = new Project(Dir, Name);
        }

        public void OpenProject(string Dir)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => OpenProject(Dir)));
                return;
            }

            if (CurrentProject != null)
            {
                CurrentProject.Dispose();
            }
            CurrentProject = new Project.Loader(Dir);
        }

        public void SaveProject()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => SaveProject()));
                return;
            }

            CurrentProject.Save();
        }

        public void SaveProjectAs(string Dir)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => SaveProjectAs(Dir)));
                return;
            }

            CurrentProject.SaveAs(Dir);
        }

        public void CloseProject()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => CloseProject()));
                return;
            }

            CurrentProject.Dispose();
            CurrentProject = null;
            ProjectClosed.Set();
        }

        public void NewString(string ID)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => NewString(ID)));
                return;
            }

            if (!CurrentProject.Strings.Select(x => x.ID).Contains(ID))
            {
                CurrentProject.Strings.Add(new Project.String()
                {
                    ID = ID,
                    Text = ID,
                    action = new Project.String.Action.Audio(),
                    ParentProject = CurrentProject,
                    Tags = new List<string>(),
                });
            }
        }

        public void NewString(Project.String newString)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => NewString(newString)));
                return;
            }

            if (!CurrentProject.Strings.Select(x => x.ID).Contains(newString.ID))
            {
                CurrentProject.Strings.Add(newString);
            }
        }
    }

    public class WindowController : Control
    {
        public Editor MainEditor = new Editor();
        public Player MainPlayer = new Player();
        public Scripter MainScripter = new Scripter();
        public Starter MainStarter = new Starter();
        public Previewer MainPreviewer = new Previewer();
        public ProgressViewer MainProgressViewer = new ProgressViewer();

        public void UpdateProgress(int Value, string Message)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => { UpdateProgress(Value, Message); }));
                return;
            }

            if (MainProgressViewer.Visible)
            {
                MainProgressViewer.Update(Value, Message);
                MainProgressViewer.Refresh();
            }
        }

        public void UpdateProgress(int Value)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => { UpdateProgress(Value); }));
                return;
            }

            if (MainProgressViewer.Visible)
            {
                MainProgressViewer.Update(Value);
                MainProgressViewer.Refresh();
            }
        }

        public void ShowProgressViewer(string Title, int Max)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => { ShowProgressViewer(Title, Max); }));
                return;
            }

            MainProgressViewer.Initialise(Title, Max);
            MainProgressViewer.Show();
        }

        public void CloseProgressViewer()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => { CloseProgressViewer(); }));
                return;
            }

            MainProgressViewer.Hide();
        }
    }
}
