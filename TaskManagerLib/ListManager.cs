using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagerLib {
    [Serializable]
    public class ListManager<T> : IListManager<T> {

        protected List<T> aList;


        /// <summary>
        /// Get number of items in list
        /// </summary>
        public int Count {
            get { return aList.Count; }
        }

        public ListManager() {
            aList = new List<T>();
        }

        /// <summary>
        /// Add item to list
        /// </summary>
        /// <param name="aType"></param>
        /// <returns></returns>
        public bool Add(T aType) {
            aList.Add(aType);
            return true;
        }

        /// <summary>
        /// Change item at postion
        /// </summary>
        /// <param name="aType"></param>
        /// <param name="aIndex"></param>
        /// <returns></returns>
        public bool ChangeAt(T aType, int aIndex) {
            if (CheckIndex(aIndex)) {
                aList[aIndex] = aType;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Check that index is in range of list
        /// </summary>
        /// <param name="aIndex"></param>
        /// <returns></returns>
        public bool CheckIndex(int aIndex) {
            return (aIndex >= 0 && aIndex < Count);
        }

        /// <summary>
        /// Clear list
        /// </summary>
        /// <returns></returns>
        public bool DeleteAll() {
            aList = new List<T>();
            return true;
        }

        /// <summary>
        /// Delete Item at Location
        /// </summary>
        /// <param name="aIndex"></param>
        /// <returns></returns>
        public bool DeleteAt(int aIndex) {
            if (CheckIndex(aIndex)) {
                aList.RemoveAt(aIndex);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Return item at location
        /// </summary>
        /// <param name="aIndex"></param>
        /// <returns></returns>
        public T GetAt(int aIndex) {
            if (CheckIndex(aIndex)) {
                return aList[aIndex];
            }
            return default(T);
        }

        /// <summary>
        /// Get items in list as string list
        /// </summary>
        /// <returns></returns>
        public List<string> ToStringList() {
            List<string> textList = new List<string>();
            foreach (T aType in aList) {
                textList.Add(aType.ToString());
            }
            return textList;
        }

        /// <summary>
        /// Return list of all
        /// </summary>
        /// <returns></returns>
        public List<T> ToList() {
            return aList;
        }

        /// <summary>
        /// Get items in list as string array
        /// </summary>
        /// <returns></returns>
        public string[] ToStringArray() {
            return ToStringList().ToArray();
        }

        /// <summary>
        /// Serialize list
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public void BinarySerialize(string fileName) {
            BinaryFormatter b = new BinaryFormatter();
            FileStream fileStream = new FileStream(fileName, FileMode.Create);
            try {
                b.Serialize(fileStream, aList);
            } finally {
                if (fileStream != null) {
                    fileStream.Close();
                }
            }
        }

        /// <summary>
        /// De-serialize list
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public virtual void BinaryDeSerialize(string fileName) {

            aList = new List<T>();
            Object obj = null;

            BinaryFormatter b = new BinaryFormatter();
            FileStream fileStream = new FileStream(fileName, FileMode.Open);
            try {
                obj = b.Deserialize(fileStream);
            } finally {
                if (fileStream != null) {
                    fileStream.Close();
                    aList = (List<T>)obj;
                }
            }
        }

    }
}
