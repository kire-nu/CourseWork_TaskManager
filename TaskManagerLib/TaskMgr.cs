using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagerLib {

    /// <summary>
    /// Class for managing tasks
    /// </summary>
    [Serializable]
    public class TaskMgr : ListManager<TaskItem> {


        public TaskMgr() { }

        /// <summary>
        /// Make sure Id is in list
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool ValidateId(int id) {
            var result = aList.Find(x => x.Id == id);
            if (result == null) {
                return true;
            } else {
                return false;
            }
        }

        /// <summary>
        /// Get highest id +1
        /// </summary>
        /// <returns></returns>
        public int GetId() {
            return aList.Select(x => x.Id).DefaultIfEmpty(-1).Max() + 1;
        }

        /// <summary>
        /// Get person(s) in project that are linked to task
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="person"></param>
        /// <returns></returns>
        public List<TaskItem> GetLinkedTasks(string projectId, string person) {
            if (aList.Count > 0) {
                // Projects that are linked to task
                var resultProj = aList.Where(x => x.LinkedTo.Keys.Contains(projectId));
                if (resultProj != null) {
                    // Persons is project that are linked to task
                    var resultPerson = resultProj.Where(x => x.LinkedTo.Values.Any(y => y.Contains(person)));
                    if (resultPerson != null) {
                        return resultPerson.ToList();
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Remove task from person in project
        /// </summary>
        /// <param name="taskItem"></param>
        /// <param name="projectId"></param>
        /// <param name="person"></param>
        public void UnlinkTask(TaskItem taskItem, string projectId, string person) {
            // Get persons linked to task
            List<String> persons = taskItem.LinkedTo[projectId];
            // Remove person from person
            persons.Remove(person);
            // Update list and item
            taskItem.LinkedTo[projectId] = persons;
            int index = aList.FindIndex(x => x.Id == taskItem.Id);
            aList[index] = taskItem;
        }


    }
}
