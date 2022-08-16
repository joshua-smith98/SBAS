using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SBAS
{
    public partial class Project
    {
        public class Sentence
        {
            public List<String> Strings = new List<String>();
            public string Source;
            Project ParentProject;
            public bool Valid = true;
            public int ErrorIndex = 0;

            public Sentence(string sentence, Project parentProject)
            {
                ParentProject = parentProject;
                Source = sentence;
                List<string> Words = sentence.GetWords().ToList();
                while (Words.Count > 0 && !ParentProject.Strings.Select(x => x.Text.ToLowerInvariant()).Any(x => string.Concat(Words).EndsWith(x)))
                {
                    Words.RemoveAt(Words.Count - 1);
                }

                List<String> ret = ParentProject.Strings.Where(x => Words.Count > x.Words.Length).ToList();
                string tempstring = string.Concat(Words);
                bool Found = false;
                List<String> newret1 = new List<String>();
                List<String> newret2 = new List<String>();
                Dictionary<String, int> BannedStrings = new Dictionary<String, int>();
                int Attempts = 0;

                while (true)
                {
                    String[] FoundStrings = ret.Where(x => tempstring.EndsWith(string.Concat(x.Words)) && (!BannedStrings.Keys.Contains(x) && !BannedStrings.Any(y => y.Key == x && y.Value == ret.FindIndex(z => z == x)))).ToArray();
                    if (FoundStrings.Length == 0)
                    {
                        if (newret1.Count == 0 || Attempts > 3) break;
                        Attempts++;
                        BannedStrings.Add(newret1.ToArray().Reverse().Last(), newret1.Count);
                        newret1.Clear();
                        tempstring = string.Concat(Words);
                    }
                    else
                    {
                        String FoundString = FoundStrings.OrderBy(x => x.Words.Length).Last();
                        int FoundStringLength = string.Concat(FoundString.Words).Length;
                        tempstring = tempstring.Remove(tempstring.Length - FoundStringLength, FoundStringLength);
                        newret1.Add(FoundString);

                        if (tempstring.Length == 0)
                        {
                            if (string.Concat(newret1.Select(x => string.Concat(x.Words).ToLower()).Reverse()) == string.Concat(Words))
                            {
                                Found = true;
                            }
                            break;
                        }
                    }
                }

                while (true)
                {
                    String[] FoundStrings = ret.Where(x => tempstring.StartsWith(string.Concat(x.Words)) && (!BannedStrings.Keys.Contains(x) && !BannedStrings.Any(y => y.Key == x && y.Value == ret.FindIndex(z => z == x)))).ToArray();
                    if (FoundStrings.Length == 0)
                    {
                        if (newret2.Count == 0 || Attempts > 3) break;
                        Attempts++;
                        BannedStrings.Add(newret2.Last(), newret2.Count);
                        newret2.Clear();
                        tempstring = string.Concat(Words);
                    }
                    else
                    {
                        String FoundString = FoundStrings.OrderBy(x => x.Words.Length).Last();
                        int FoundStringLength = string.Concat(FoundString.Words).Length;
                        tempstring = tempstring.Remove(tempstring.Length - FoundStringLength, FoundStringLength);
                        newret2.Add(FoundString);

                        if (tempstring.Length == 0)
                        {
                            if (string.Concat(newret2.Select(x => string.Concat(x.Words).ToLower())) == string.Concat(Words))
                            {
                                Found = true;
                            }
                            break;
                        }
                    }
                }

                if (Found)
                {
                    if (newret1.Count > 0 && newret2.Count == 0) Strings = newret1.ToArray().Reverse().ToList();
                    else if (newret2.Count > 0 && newret1.Count == 0) Strings = newret2;
                    else if (newret1.Select(x => x.Words.Length).Average() > newret2.Select(x => x.Words.Length).Average()) Strings = newret1.ToArray().Reverse().ToList();
                    else Strings = newret2;
                }
                else
                {
                    Valid = false;
                    ErrorIndex = string.Concat(Words).Length - tempstring.Length;
                }
            }

            public void Save(string dir)
            {
                if (Valid)
                {
                    List<AudioFile> audioFiles = new List<AudioFile>();
                    foreach (String str in Strings) audioFiles.Add(str.GetAudioFile());
                    AudioFile MainAudio = AudioFile.Combine(audioFiles.ToArray());
                    FileStream SavedAudio = File.Open(dir, FileMode.CreateNew, FileAccess.Write, FileShare.None);
                    MainAudio.fileStream.Position = 0;
                    MainAudio.fileStream.CopyTo(SavedAudio);
                    SavedAudio.Dispose();
                }
            }

            public void Play()
            {
                if (Valid)
                {
                    List<AudioFile> audioFiles = new List<AudioFile>();
                    foreach (String str in Strings) audioFiles.Add(str.GetAudioFile());
                    AudioFile MainAudio = AudioFile.Combine(audioFiles.ToArray());
                    MainAudio.Play();
                }
            }
        }
    }
}
