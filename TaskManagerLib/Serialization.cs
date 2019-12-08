using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagerLib {

    /// <summary>
    /// Class to save to and read from file
    /// </summary>
    [Serializable]
    public class Serialization {

        // Data to be saved
        private TaskMgr taskManager;
        private ProjectMgr projectManager;

        /// <summary>
        /// Serialize (save)
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="taskManager"></param>
        /// <param name="projectManager"></param>
        public void BinarySerialize(string fileName, TaskMgr taskManager, ProjectMgr projectManager) {
            this.taskManager = taskManager;
            this.projectManager = projectManager;
            BinaryFormatter b = new BinaryFormatter();
            FileStream fileStream = new FileStream(fileName, FileMode.Create);
            try {
                b.Serialize(fileStream, this);
            } finally {
                if (fileStream != null) {
                    fileStream.Close();
                }
            }
        }

        /// <summary>
        /// DeSerialize (load)
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="taskManager"></param>
        /// <param name="projectManager"></param>
        public void BinaryDeSerialize(string fileName, out TaskMgr taskManager, out ProjectMgr projectManager) {
            taskManager = null;
            projectManager = null;
            Serialization serialization;
            Object obj = null;

            BinaryFormatter b = new BinaryFormatter();
            FileStream fileStream = new FileStream(fileName, FileMode.Open);
            try {
                obj = b.Deserialize(fileStream);
                serialization = (Serialization)obj;
                if (serialization != null) {
                    projectManager = serialization.projectManager;
                    taskManager = serialization.taskManager;
                }
            } finally {
                if (fileStream != null) {
                    fileStream.Close();

                }
            }
        }
    }
}
