using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagerLib {

    /// <summary>
    /// Project items
    /// </summary>
    [Serializable]
    public class ProjectItem {

        private string id;
        private string projectName;
        private List<string> person = new List<string>(); // list of persons

        public ProjectItem(string projectNumber, string projectName) {
            this.id = projectNumber;
            this.projectName = projectName;
        }

        public List<string> Persons {
            get { return person; }
            set { person = value; }
        }

        public string Id {
            get { return id; }
        }

        public string ProjectName {
            get => projectName; set => projectName = value;
        }
    }
}
