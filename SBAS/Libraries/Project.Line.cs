using System.Collections.Generic;
using System.Linq;

namespace SBAS
{
    public partial class Project
    {
        public class Line
        {
            public class Stop
            {
                public String Name;
                public Project ParentProject;

                public Stop(String Name, Project ParentProject)
                {
                    this.Name = Name;
                    this.ParentProject = ParentProject;
                }

                internal Stop() { }

                public Line GetParentLine()
                {
                    return ParentProject.Lines.Find(x => x.Stops.Contains(this));
                }

                public override string ToString()
                {
                    return Name.ID;
                }
            }

            public string Name;
            public Project ParentProject;
            public List<Stop> Stops = new List<Stop>();

            public Line BranchesFrom;
            internal string BranchesFromStr;
            public Stop BranchesAt;
            internal string BranchesAtStr;
            public Line ConnectsTo;
            internal string ConnectsToStr;
            public Stop ConnectsAt;
            internal string ConnectsAtStr;

            public Line(string Name, Project ParentProject)
            {
                this.Name = Name;
                this.ParentProject = ParentProject;
            }

            internal Line() { }

            internal void Initialise()
            {
                if (BranchesFromStr != null) BranchesFrom = ParentProject.Lines.Find(x => x.Name == BranchesFromStr);
                if (ConnectsToStr != null) ConnectsTo = ParentProject.Lines.Find(x => x.Name == ConnectsToStr);

                if (BranchesAtStr != null) BranchesAt = ParentProject.Stops.Find(x => x.Name.ID == BranchesAtStr);
                if (ConnectsAtStr != null) ConnectsAt = ParentProject.Stops.Find(x => x.Name.ID == ConnectsAtStr);
            }

            public String GetLineString()
            {
                return ParentProject.Strings.Find(x => x.Text == Name);
            }

            public Line[] GetBranches()
            {
                return ParentProject.Lines.Where(x => x.BranchesFrom == this || x.ConnectsTo == this).ToArray();
            }

            public void MoveStopUp(Stop stop)
            {
                if (!Stops.Contains(stop)) return;
                if (Stops.IndexOf(stop) == 0) return;

                int StopIndex = Stops.IndexOf(stop);
                Stops.Remove(stop);
                Stops.Insert(StopIndex - 1, stop);
            }

            public void MoveStopDown(Stop stop)
            {
                if (!Stops.Contains(stop)) return;
                if (Stops.IndexOf(stop) == Stops.Count - 1) return;

                int StopIndex = Stops.IndexOf(stop);
                Stops.Remove(stop);
                Stops.Insert(StopIndex + 1, stop);
            }

            public override string ToString()
            {
                return Name;
            }
        }
    }
}
