using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;

namespace SBAS
{
    public partial class Project
    {
        public class AudioFile
        {
            public MemoryStream fileStream;
            public WaveFormat waveFormat;
            internal string Hash;
            public const string NullHash = "00000000000000000000000000000000";
            public long Offset;
            public long Length;

            internal bool AutoCropping = false;
            public bool AutoCroppingEnabled { get { return AutoCropping; } }
            public long CroppingL
            {
                get
                {
                    if (AutoCropping)
                    {
                        return (long)((AutoCroppingL + ManCroppingL) / (waveFormat.SampleRate / 1000f));
                    }
                    else
                    {
                        return (long)((ManCroppingL) / (waveFormat.SampleRate / 1000f));
                    }
                }
                set
                {
                    ManCroppingL = (long)(value * (waveFormat.SampleRate / 1000f));
                }
            }
            public long CroppingR
            {
                get
                {
                    if (AutoCropping)
                    {
                        return (long)((AutoCroppingR + ManCroppingR) / (waveFormat.SampleRate / 1000f));
                    }
                    else
                    {
                        return (long)((ManCroppingR) / (waveFormat.SampleRate / 1000f));
                    }
                }
                set
                {
                    ManCroppingR = (long)(value * (waveFormat.SampleRate / 1000f));
                }
            }

            public long CroppingLSamples
            {
                get
                {
                    if (AutoCropping)
                    {
                        return AutoCroppingL + ManCroppingL;
                    }
                    else
                    {
                        return ManCroppingL;
                    }
                }
                set
                {
                    ManCroppingL = value;
                }
            }
            public long CroppingRSamples
            {
                get
                {
                    if (AutoCropping)
                    {
                        return AutoCroppingR + ManCroppingR;
                    }
                    else
                    {
                        return ManCroppingR;
                    }
                }
                set
                {
                    ManCroppingR = value;
                }
            }
            long AutoCroppingL = 0;
            long AutoCroppingR = 0;
            internal long ManCroppingL = 0;
            internal long ManCroppingR = 0;
            public long OffsetL { get { return (long)(ManCroppingL / (waveFormat.SampleRate / 1000f)); } }
            public long OffsetR { get { return (long)(ManCroppingR / (waveFormat.SampleRate / 1000f)); } }
            const float AutoCropThreshold = 0.00085f;
            const short AutoCropResolution = 100;

            internal Project ParentProject;

            public string FullDir;
            public string Directory { get { return Path.GetDirectoryName(FullDir); } }
            public string FileName { get { return Path.GetFileName(FullDir); } }
            public string Extension { get { return Path.GetExtension(FullDir); } }

            public bool IsInDataFile;

            internal AudioFile() { }

            public AudioFile(string Dir, Project parentProject)
            {
                FileStream filestream = File.Open(Dir, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                filestream.Seek(0, SeekOrigin.Begin);
                fileStream = new MemoryStream();
                filestream.CopyTo(fileStream);
                Offset = 0;
                Length = filestream.Length;
                fileStream.Position = 0;
                Hash = BitConverter.ToString(MD5.Create().ComputeHash(fileStream)).Replace("-", "").ToLowerInvariant();
                fileStream.Position = 0;
                IsInDataFile = false;
                FullDir = filestream.Name;
                waveFormat = new WaveFileReader(fileStream).WaveFormat;
                fileStream.Position = 0;
                ParentProject = parentProject;
            }

            public AudioFile(Stream filestream, WaveFormat waveformat, Project parentProject = null)
            {
                fileStream = new MemoryStream();
                filestream.Seek(0, SeekOrigin.Begin);
                filestream.CopyTo(fileStream);
                Offset = 0;
                Length = filestream.Length;
                fileStream.SetLength(Length);
                Hash = BitConverter.ToString(MD5.Create().ComputeHash(fileStream)).Replace("-", "").ToLowerInvariant();
                fileStream.Position = 0;
                IsInDataFile = false;
                FullDir = null;
                waveFormat = waveformat;
                fileStream.Seek(0, SeekOrigin.Begin);
                ParentProject = parentProject;
            }

            internal void Initialise()
            {
                if (fileStream == null)
                {
                    fileStream = new MemoryStream();
                    File.Open(FullDir, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite).CopyBytes(fileStream, Offset, Length);
                    if (Hash == null) Hash = BitConverter.ToString(MD5.Create().ComputeHash(fileStream)).Replace("-", "").ToLowerInvariant();
                }
                fileStream.Position = 0;
                waveFormat = new WaveFileReader(fileStream).WaveFormat;
                fileStream.Position = 0;
                if (AutoCropping) CalculateAutoCropping();
            }

            public static AudioFile CreateNullAudioFile()
            {
                AudioFile ret = new AudioFile();

                ret.fileStream = new MemoryStream();
                RawSourceWaveStream EmptyFile = new RawSourceWaveStream(new MemoryStream(), new WaveFormat(44100, 16, 1));
                WaveFileWriter.WriteWavFileToStream(ret.fileStream, EmptyFile);
                ret.Offset = 0;
                ret.Length = ret.fileStream.Length;
                ret.Hash = BitConverter.ToString(MD5.Create().ComputeHash(ret.fileStream)).Replace("-", "").ToLowerInvariant();
                ret.IsInDataFile = false;
                ret.FullDir = null;
                ret.waveFormat = new WaveFormat(44100, 16, 1);
                ret.fileStream.Seek(0, SeekOrigin.Begin);

                return ret;
            }

            public void EnableAutoCropping()
            {
                CalculateAutoCropping();
                AutoCropping = true;
            }

            public void DisableAutoCropping()
            {
                AutoCropping = false;
            }

            public void Play()
            {
                fileStream.Seek(0, SeekOrigin.Begin);
                new Audio.MemoryPlayer(fileStream).PlayAsync();
            }

            public static AudioFile Combine(AudioFile[] audioFiles, bool autoCropping = true)
            {
                SBAS.MainWindowController.ShowProgressViewer("Rendering Audio...", audioFiles.Length * 2);
                int Counter = 0;
                audioFiles[0].fileStream.Seek(0, SeekOrigin.Begin);
                WaveFormat Format = new WaveFileReader(audioFiles[0].fileStream).WaveFormat;
                List<AudioFile> audioFiles2 = new List<AudioFile>();
                foreach (AudioFile audioFile in audioFiles)
                {
                    SBAS.MainWindowController.UpdateProgress(Counter++, "Converting");
                    audioFile.fileStream.Seek(0, SeekOrigin.Begin);
                    WaveFileReader reader = new WaveFileReader(audioFile.fileStream);
                    MediaFoundationResampler resampler = new MediaFoundationResampler(reader, Format);
                    MemoryStream NewWave = new MemoryStream();
                    WaveFileWriter.WriteWavFileToStream(NewWave, resampler);
                    AudioFile newAudioFile = new AudioFile(NewWave, Format);
                    newAudioFile.ManCroppingL = audioFile.ManCroppingL;
                    newAudioFile.ManCroppingR = audioFile.ManCroppingR;
                    newAudioFile.AutoCropping = audioFile.AutoCropping;
                    newAudioFile.Initialise();
                    audioFiles2.Add(newAudioFile);
                }
                Array CombinedSamples = Array.CreateInstance(typeof(float[]), audioFiles2.Select(x => x.Length - x.CroppingLSamples - x.CroppingRSamples).Sum() * 2);
                long CroppingVal = 0;
                long CurrentOffset = 0;
                foreach (AudioFile audioFile in audioFiles2)
                {
                    SBAS.MainWindowController.UpdateProgress(Counter++, "Compiling");
                    audioFile.fileStream.Position = 0;
                    WaveFileReader WfR = new WaveFileReader(audioFile.fileStream);
                    if (audioFile != audioFiles2.First())
                    {
                        CroppingVal += audioFile.CroppingLSamples;
                        CurrentOffset -= CroppingVal;

                        for (long i = 0; i < CroppingVal; i++)
                        {
                            float[] NewSample = WfR.ReadNextSampleFrame();
                            if (NewSample == null) NewSample = Enumerable.Repeat(0f, Format.Channels).ToArray();

                            float[] OldSample = (float[])CombinedSamples.GetValue(CurrentOffset);
                            if (OldSample == null) OldSample = Enumerable.Repeat(0f, Format.Channels).ToArray();

                            float[] CombinedSample = NewSample.Zip(OldSample, (x, y) => (x + y)).ToArray();
                            CombinedSamples.SetValue(CombinedSample, CurrentOffset);
                            CurrentOffset++;
                        }
                    }
                    for (long i = 0; i < WfR.SampleCount - CroppingVal; i++)
                    {
                        float[] Sample = WfR.ReadNextSampleFrame();
                        if (Sample == null) Sample = Enumerable.Repeat(0f, Format.Channels).ToArray();
                        CombinedSamples.SetValue(Sample, CurrentOffset);
                        CurrentOffset++;
                    }
                    CroppingVal = audioFile.CroppingRSamples;
                }

                SBAS.MainWindowController.CloseProgressViewer();

                MemoryStream retStream = new MemoryStream();
                MemoryStream retWave = new MemoryStream();
                WaveFileWriter Writer = new WaveFileWriter(retStream, Format);
                for (long i = 0; i < CombinedSamples.LongLength; i++)
                {
                    float[] Sample = (float[])CombinedSamples.GetValue(i);
                    if (Sample != null) Writer.WriteSamples(Sample, 0, Sample.Length);
                }
                retStream.Seek(0, SeekOrigin.Begin);
                RawSourceWaveStream retRaw = new RawSourceWaveStream(retStream, Format);
                retRaw.Seek(0, SeekOrigin.Begin);
                WaveFileWriter.WriteWavFileToStream(retWave, retRaw);

                AudioFile ret = new AudioFile(retWave, Format) { AutoCropping = autoCropping };
                ret.Initialise();
                return ret;
            }

            internal void CalculateAutoCropping()
            {
                fileStream.Seek(0, SeekOrigin.Begin);

                WaveFileReader WfR = new WaveFileReader(fileStream);
                float Peak = 0;

                for (long i = 0; i < WfR.SampleCount; i++)
                {
                    float Sample = Math.Abs(WfR.ReadNextSampleFrame().Average());
                    if (Sample != 0 && Sample >= Peak) Peak = Sample;
                }

                WfR.Position = 0;

                bool LFinished = false;
                bool RFinished = false;
                for (long i = 0; i < WfR.SampleCount; i++)
                {
                    float Sample = Math.Abs(WfR.ReadNextSampleFrame().Average());
                    if (i % AutoCropResolution == 0)
                    {
                        if (!LFinished && Sample / Peak > AutoCropThreshold)
                        {
                            LFinished = true;
                            AutoCroppingL = i;
                        }
                        else if (!RFinished && Sample / Peak < AutoCropThreshold)
                        {
                            RFinished = true;
                            AutoCroppingR = (WfR.SampleCount) - i;
                        }
                        else if (RFinished && Sample / Peak > AutoCropThreshold) RFinished = false;
                    }
                }
            }

            public DataFile GetDataFile()
            {
                if (IsInDataFile) return ParentProject.DataFiles.Find(x => x.AudioFiles.Contains(this));
                else return null;
            }

            public override string ToString()
            {
                return FileName;
            }
        }
    }
}
