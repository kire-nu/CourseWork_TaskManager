using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagerLib {

    /// <summary>
    /// Task item
    /// </summary>
    [Serializable]
    public class TaskItem {

        private int id;
        private string name;
        private string description = string.Empty;
        private Dictionary<string,List<string>> linkedTo = new Dictionary<string, List<string>>(); // Link to project and person

        public TaskItem(int id, string name, string description) {
            this.id = id;
            this.name = name;
            this.description = description;
        }

        public Dictionary<string,List<string>> LinkedTo {
            get { return linkedTo; }
            set { linkedTo = value; }
        }

        public string Name {
            get { return name; }
            set { name = value; }
        }

        public string Description {
            get { return description; }
            set { description = value; }
        }

        public int Id {
            get { return id; }
        }

        // Create link to person and project
        public void AddToLinkProjectAndPerson(string projectId, string personName) {
            if (linkedTo.Count > 0) {
                // Get project if exist in link, if not we need to create a list
                var resultProj = linkedTo.Single(x => x.Key == projectId);
                if (resultProj.Value != null) {
                    // Get person, if not exist, we need to add to list
                    var resultPerson = resultProj.Value.Single(x => string.Compare(x, personName) == 0);
                    if (resultPerson != null) {
                        return;
                    } else {
                        // Create link
                        resultProj.Value.Add(personName);
                        linkedTo[projectId] = resultProj.Value;
                        return;
                    }
                }
            }
            List<string> newList = new List<string>();
            newList.Add(personName);
            linkedTo.Add(projectId, newList);
        }


    }
}
