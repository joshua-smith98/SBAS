using System.Collections.Generic;
using System.Linq;

namespace SBAS
{
    public class Indexer
    {
        public class File
        {
            public Folder Directory;
            public string Path;

            public File(string Path)
            {
                Directory = null;
                this.Path = Path;
            }

            internal File(string Path, Folder Directory)
            {
                this.Path = Path;
                this.Directory = Directory;
            }

            public Folder[] GetParents()
            {
                if (Directory == null) return null;

                Folder CurrentDir = Directory;
                List<Folder> ret = new List<Folder>();

                while (CurrentDir.Directory != null)
                {
                    ret.Add(CurrentDir);
                    CurrentDir = CurrentDir.Directory;
                }

                return ret.ToArray();
            }
        }

        public class Folder
        {
            public List<File> Files = new List<File>();
            public List<Folder> Folders = new List<Folder>();
            public Folder Directory;
            public string Path;

            public Folder(string Path)
            {
                Directory = null;
                this.Path = Path;

                string[] SubFolders = System.IO.Directory.GetDirectories(Path);
                string[] SubFiles = System.IO.Directory.GetFiles(Path);

                foreach (string SubFolder in SubFolders) Folders.Add(new Folder(SubFolder, this));
                foreach (string SubFile in SubFiles) Files.Add(new File(SubFile, this));
            }

            internal Folder(string Path, Folder Directory)
            {
                this.Path = Path;
                this.Directory = Directory;

                string[] SubFolders = System.IO.Directory.GetDirectories(Path);
                string[] SubFiles = System.IO.Directory.GetFiles(Path);

                foreach (string SubFolder in SubFolders) Folders.Add(new Folder(SubFolder, this));
                foreach (string SubFile in SubFiles) Files.Add(new File(SubFile, this));
            }

            public Folder[] GetParents()
            {
                if (Directory == null) return null;

                Folder CurrentDir = Directory;
                List<Folder> ret = new List<Folder>();

                while (CurrentDir != null)
                {
                    ret.Add(CurrentDir);
                    CurrentDir = CurrentDir.Directory;
                }

                return ret.ToArray();
            }

            public File[] GetChildren(string Format)
            {
                List<File> ret = new List<File>();

                foreach (Folder SubFolder in Folders)
                {
                    ret.AddRange(GetChildren(SubFolder));
                }

                ret.AddRange(Files);

                ret = ret.Where(x => System.IO.Path.GetExtension(x.Path).ToLower() == Format.ToLower()).ToList();

                return ret.ToArray();
            }

            static internal File[] GetChildren(Folder CurrentFolder)
            {
                List<File> ret = new List<File>();

                foreach (Folder SubFolder in CurrentFolder.Folders)
                {
                    ret.AddRange(GetChildren(SubFolder));
                }

                ret.AddRange(CurrentFolder.Files);

                return ret.ToArray();
            }

            public Folder[] GetSubFolders()
            {
                List<Folder> ret = new List<Folder>();

                foreach (Folder SubFolder in Folders)
                {
                    ret.AddRange(GetSubFolders(SubFolder));
                }

                return ret.ToArray();
            }

            static internal Folder[] GetSubFolders(Folder CurrentFolder)
            {
                List<Folder> ret = new List<Folder>();

                foreach (Folder SubFolder in CurrentFolder.Folders)
                {
                    ret.AddRange(GetSubFolders(SubFolder));
                }

                return ret.ToArray();
            }
        }

    }
}
