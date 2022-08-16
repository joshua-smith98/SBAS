namespace SBAS
{
    public partial class Project
    {
        public partial class String
        {
            public class Action
            {
                public class Audio : Action
                {
                    internal string MainAudioHash = AudioFile.NullHash;
                    internal string EndAudioHash = AudioFile.NullHash;
                    public AudioFile MainAudio;
                    public AudioFile EndAudio;

                    public bool AllowPhrasing = true;

                    public Audio()
                    {
                        value = 0;
                    }

                    public Audio(AudioFile MainAudio)
                    {
                        value = 0;
                        this.MainAudio = MainAudio;
                        MainAudioHash = MainAudio.Hash;
                    }
                }

                public class Delay : Action
                {
                    public short DelayLength;

                    public Delay()
                    {
                        value = 2;
                    }
                }

                internal short value;
            }

            public static class Actions
            {
                public static readonly Action Audio = new Action() { value = 0 };
                public static readonly Action Group = new Action() { value = 1 };
                public static readonly Action Delay = new Action() { value = 2 };
            }
        }
    }
}
