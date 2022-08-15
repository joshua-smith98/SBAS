using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;
using System.Windows.Forms;
using NAudio;
using NAudio.Wave;

namespace SBAS
{
    public class Audio
    {
        public class FilePlayer
        {
            FileStream File;
            WaveFileReader WvR;
            WaveChannel32 WCH;
            WaveOutEvent player;
            bool DeleteOnStop = false;

            public FilePlayer(FileStream file)
            {
                File = file;
                WvR = new WaveFileReader(file);
                WCH = new WaveChannel32(WvR);
                player = new WaveOutEvent();
                WCH.PadWithZeroes = false;
                player.Init(WCH);
                player.PlaybackStopped += new EventHandler<StoppedEventArgs>(OnPlaybackStopped);
            }

            public void PlayAsync(bool Deleteonstop)
            {
                DeleteOnStop = Deleteonstop;
                player.Play();
            }

            void OnPlaybackStopped(object sender, StoppedEventArgs e)
            {
                if (DeleteOnStop)
                {
                    string FileName = File.Name;
                    player.Dispose();
                    WCH.Dispose();
                    WvR.Close();
                    WvR.Dispose();
                    File.Close();
                    File.Dispose();
                    System.IO.File.Delete(FileName);
                }
            }
        }

        public class MemoryPlayer
        {
            MemoryStream File;
            WaveFileReader WvR;
            WaveChannel32 WCH;
            WaveOutEvent player;

            internal MemoryPlayer() { }

            public MemoryPlayer(MemoryStream file)
            {
                File = file;
                File.Seek(0, SeekOrigin.Begin);
                WvR = new WaveFileReader(File);
                WCH = new WaveChannel32(WvR);
                player = new WaveOutEvent();
                WCH.PadWithZeroes = false;
                player.Init(WCH);
            }

            public void PlayAsync(int Delay = 0)
            {
                Thread.Sleep(Delay);
                player.Play();
            }

            public class Group : MemoryPlayer
            {
                Dictionary<int, MemoryPlayer> Players;

                public Group(Dictionary<int, MemoryPlayer> players)
                {
                    Players = players;
                }

                public void PlayAsync()
                {
                    foreach (KeyValuePair<int, MemoryPlayer> pl in Players)
                    {
                        pl.Value.PlayAsync(pl.Key);
                    }
                }
            }
        }
    }
}
