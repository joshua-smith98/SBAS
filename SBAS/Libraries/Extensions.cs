using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SBAS
{
    public static class Extensions
    {
        public static long FindFirst(this BinaryReader br, string Value)
        {
            string Temp = String.Empty;
            char tempChar;
            while (br.BaseStream.Position < br.BaseStream.Length)
            {
                byte[] charbuffer = br.ReadBytes(1);
                tempChar = Encoding.ASCII.GetChars(charbuffer).First();
                if (tempChar == Value[Temp.Length]) Temp += tempChar;
                else Temp = String.Empty;

                if (Temp == Value) return br.BaseStream.Position - Value.Length;
            }

            return -1;
        }

        public static void CopyBytes(this FileStream fs, Stream newfile, long offset, long count)
        {
            long MaxValue = offset + count;
            long CurrentOffset = 0;
            fs.Seek(offset, SeekOrigin.Begin);
            newfile.Seek(0, SeekOrigin.End);
            BinaryReader br = new BinaryReader(fs);
            BinaryWriter bw = new BinaryWriter(newfile);

            if (count > int.MaxValue)
            {
                while (CurrentOffset + int.MaxValue < count)
                {
                    bw.Write(br.ReadBytes(int.MaxValue));
                    CurrentOffset += int.MaxValue;
                }
                bw.Write(br.ReadBytes((int)(count - CurrentOffset)));
            }
            else
            {
                bw.Write(br.ReadBytes((int)count));
            }
        }

        public static void Synchronise(this ListBox.ObjectCollection ThisCollection, IEnumerable<object> Collection)
        {
            List<object> ReferenceList = ThisCollection.Cast<object>().ToList();
            List<object> SyncList = Collection.Cast<object>().ToList();

            if (SyncList.Exists(x => !ReferenceList.Contains(x)))
            {
                List<object> NewItems = SyncList.FindAll(x => !ReferenceList.Contains(x));

                ThisCollection.AddRange(NewItems.ToArray());
            }

            if (ReferenceList.Exists(x => !SyncList.Contains(x)))
            {
                List<object> OldItems = ReferenceList.FindAll(x => !SyncList.Contains(x));
                foreach (object item in OldItems) ThisCollection.Remove(item);
            }
        }

        public static void RefreshSelected(this ListBox ThisListBox)
        {
            object SelectedObject = ThisListBox.SelectedItem;

            if (SelectedObject == null) return;
            ThisListBox.Items.Remove(SelectedObject);
            ThisListBox.Items.Add(SelectedObject);
            ThisListBox.SelectedItem = SelectedObject;
        }

        public static void Synchronise(this TreeView treeView, Dictionary<Project.AudioFile, string> paths)
        {
            TreeNode CurrentNode;
            int Depth = 0;
            Dictionary<Project.AudioFile, string[]> SplitPaths = paths.ToDictionary(x => x.Key, x => x.Value.Split('\\'));

            foreach (KeyValuePair<Project.AudioFile, string[]> pair in SplitPaths)
            {
                if (!treeView.Nodes.ContainsKey(pair.Value[0]))
                {
                    TreeNode NewNode = new TreeNode(pair.Value[0]) { Tag = 1 == pair.Value.Length ? pair.Key : null, Name = pair.Value[0], Text = pair.Value[0] };
                    treeView.Nodes.Add(NewNode);
                }
            }

            foreach (TreeNode node in treeView.Nodes)
            {
                foreach (KeyValuePair<Project.AudioFile, string[]> pair in SplitPaths)
                {
                    Depth = 1;
                    CurrentNode = node;
                    for (int i = 1; i < pair.Value.Length; i++)
                    {
                        Depth++;
                        if (CurrentNode.Nodes.ContainsKey(pair.Value[i]))
                        {
                            CurrentNode = CurrentNode.Nodes[CurrentNode.Nodes.IndexOfKey(pair.Value[i])];
                        }
                        else
                        {
                            TreeNode NewNode = new TreeNode(pair.Value[i]) { Tag = i == pair.Value.Length - 1 ? pair.Key : null, Name = pair.Value[i], Text = pair.Value[i] };
                            CurrentNode.Nodes.Add(NewNode);
                            CurrentNode = NewNode;
                        }
                    }
                }

            }

            foreach (TreeNode node in treeView.GetAllNodes())
            {
                if (node.TreeView != null && !paths.Values.Any(x => x.Contains(node.FullPath))) node.Remove();
            }
        }

        public static TreeNode[] GetChildren(this TreeNode Node)
        {
            List<TreeNode> ret = new List<TreeNode>();
            foreach (TreeNode node in Node.Nodes)
            {
                ret.Add(node);
                ret.AddRange(node.GetChildren());
            }

            return ret.ToArray();
        }

        public static TreeNode[] GetAllNodes(this TreeView Node)
        {
            List<TreeNode> ret = new List<TreeNode>();
            foreach (TreeNode node in Node.Nodes)
            {
                ret.Add(node);
                ret.AddRange(node.GetChildren());
            }

            return ret.ToArray();
        }

        public static string MakeString(this char[] chars)
        {
            return new string(chars);
        }

        public static string[] GetWords(this string Sentence)
        {
            List<string> ret = new List<string>();
            string NewSentence = String.Empty;

            if (Sentence.Any(x => Char.IsWhiteSpace(x)))
            {
                bool toUpper = false;
                for (int i = 0; i < Sentence.Length; i++)
                {
                    if (Char.IsWhiteSpace(Sentence[i]))
                    {
                        toUpper = true;
                    }
                    else
                    {
                        if (toUpper)
                        {
                            NewSentence += Char.ToUpperInvariant(Sentence[i]);
                            toUpper = false;
                        }
                        else NewSentence += Sentence[i];
                    }
                }
                Sentence = NewSentence;
            }

            if (Sentence.Count(x => Char.IsUpper(x)) > 0 && Sentence.Count(x => Char.IsLower(x)) > Sentence.Count(x => Char.IsUpper(x)))
            {
                string TempString = String.Empty;

                foreach (char chr in Sentence)
                {
                    if ((Char.IsUpper(chr) || Char.IsPunctuation(chr) || Char.IsSymbol(chr)) && TempString.Length != 0)
                    {
                        ret.Add(TempString);
                        TempString = String.Empty;
                    }
                    TempString += chr;
                }
                if (TempString.Length != 0) ret.Add(TempString);
                return ret.Select(x => x.ToLowerInvariant()).ToArray();
            }
            else
            {
                ret.Add(Sentence);
                return ret.Select(x => x.ToLowerInvariant()).ToArray();
            }
        }

        public static long CopyTo(this Stream inStream, Stream outStream, long bytesRequired)
        {
            long readSoFar = 0L;
            var buffer = new byte[64 * 1024];
            do
            {
                var toRead = Math.Min(bytesRequired - readSoFar, buffer.Length);
                var readNow = inStream.Read(buffer, 0, (int)toRead);
                if (readNow == 0)
                    break; // End of stream
                outStream.Write(buffer, 0, readNow);
                readSoFar += readNow;
            } while (readSoFar < bytesRequired);
            return readSoFar;
        }

        public static string GetUpperFileName(this string path)
        {
            bool Success = false;
            while (path != string.Empty)
            {
                path = Path.GetDirectoryName(path);
                if (Path.GetFileName(path) != string.Empty)
                {
                    Success = true;
                    break;
                }
            }

            return path;
        }
    }
}

