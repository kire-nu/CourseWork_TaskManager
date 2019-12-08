using Microsoft.VisualStudio.TestTools.UnitTesting;
using TaskManagerLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagerLib.Tests
{
    [TestClass()]
    public class TaskMgrTests
    {
        [TestMethod()]
        public void UnlinkTaskTest()
        {

            //Arrange
            TaskMgr taskMgr = new TaskMgr();
            ProjectMgr projectMgr = new ProjectMgr();
            TaskItem taskItem = new TaskItem(1, "Test", "Test Item");
            taskMgr.Add(taskItem);

            //Act
            taskMgr.UnlinkTask(taskItem, null, null);

            //Assess
            Assert.Fail();
        }

        [TestMethod()]
        public void GetLinkedTasksTest()
        {

            //Arrange
            TaskMgr taskMgr = new TaskMgr();
            ProjectMgr projectMgr = new ProjectMgr();

            //Act
            List<TaskItem> taskItems = taskMgr.GetLinkedTasks(null, null);

            //Assess
            Assert.AreEqual(null, taskItems);

        }
    }
}