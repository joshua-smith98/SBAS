using System.Collections.Generic;
using System.Linq;

namespace SBAS
{
    public partial class Project
    {
        public partial class String
        {
            public string ID;
            public string Text;
            public string[] Words { get { return Text.GetWords(); } }
            public List<string> Tags = new List<string>();
            public Action action;

            internal Project ParentProject;

            internal String() { }

            internal void Initialise()
            {
                if (ParentProject.Strings.Exists(x => x.ID == ID && x != this)) throw new DuplicateStringException();

                if (action.value == 0)
                {
                    AudioFile FoundAudio = ParentProject.AudioFiles.Find(x => x.Hash == ((Action.Audio)action).MainAudioHash);
                    if (FoundAudio != null) ((Action.Audio)action).MainAudio = FoundAudio;

                    FoundAudio = ParentProject.AudioFiles.Find(x => x.Hash == ((Action.Audio)action).EndAudioHash);
                    if (FoundAudio != null) ((Action.Audio)action).EndAudio = FoundAudio;
                }
            }

            public String[] GetChildren()
            {
                List<String> ret = ParentProject.Strings.Where(x => x != this && Words.Length > x.Words.Length).ToList();
                //string tempstring = string.Concat(Words);
                string[] tempwords = Words;
                bool Found = false;
                List<String> newret = new List<String>();

                while (true)
                {
                    //String[] FoundStrings = ret.Where(x => tempstring.StartsWith(string.Concat(x.Words))).ToArray();
                    String[] FoundStrings = ret.Where(x => string.Concat(tempwords.Reverse().Take(x.Words.Length).Reverse()) == string.Concat(x.Words)).ToArray();
                    if (FoundStrings.Length == 0) break;
                    else
                    {
                        //String FoundString = FoundStrings.OrderBy(x => x.Words.Length).Last();
                        String FoundString = FoundStrings.ToList().Find(x => string.Concat(tempwords.Reverse().Take(x.Words.Length).Reverse()) == string.Concat(x.Words) && !FoundStrings.Any(y => y != x && string.Concat(y.Words) == string.Concat(x.Words) && y.Words.Length < x.Words.Length));
                        //tempstring = tempstring.Remove(0, FoundString.Text.Length);
                        tempwords = tempwords.Reverse().Skip(FoundString.Words.Length).Reverse().ToArray();
                        newret.Add(FoundString);

                        if (tempwords.Length == 0)
                        {
                            if (string.Concat(newret.ToArray().Reverse().Select(x => string.Concat(x.Words).ToLower())) == string.Concat(Words))
                            {
                                Found = true;
                            }
                            break;
                        }
                    }
                }

                if (Found) return newret.ToArray().Reverse().ToArray();
                else return new List<String>().ToArray();
            }

            public String[] GetParents()
            {
                return ParentProject.Strings.Where(x => x.Text.ToLowerInvariant().Contains(Text.ToLowerInvariant()) && x.GetChildren().Contains(this)).ToArray();
            }

            public void Play()
            {
                GetAudioFile().Play();
            }

            public AudioFile GetAudioFile()
            {
                if (action.value == Actions.Audio.value)
                {
                    return ((Action.Audio)action).MainAudio;
                }
                else if (action.value == Actions.Group.value)
                {
                    String[] children = GetChildren();
                    List<AudioFile> audioFiles = new List<AudioFile>();

                    foreach (String str in children)
                    {
                        audioFiles.Add(str.GetAudioFile());
                    }

                    return AudioFile.Combine(audioFiles.ToArray());
                }
                else if (action.value == Actions.Delay.value)
                {
                    AudioFile Delay = AudioFile.CreateNullAudioFile();
                    Delay.CroppingL = -((Action.Delay)action).DelayLength;
                    return Delay;
                }
                else return null;
            }

            public override string ToString()
            {
                return ID;
            }
        }
    }
}
