using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagerLib {

    /// <summary>
    /// Class for managing project
    /// </summary>
    [Serializable]
    public class ProjectMgr : ListManager<ProjectItem> {

        /// <summary>
        /// Make sure ID is in list
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool ValidateId(string id) {
            var result = aList.Find(x => x.Id == id);
            if (result == null) {
                return true;
            } else {
                return false;
            }
        }

    }
}
