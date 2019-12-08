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
    public class ProjectMgrTests
    {
        [TestMethod()]
        public void ValidateIdTest()
        {

            //Arrange
            ProjectMgr projectMgr = new ProjectMgr();
            ProjectItem projectItem = new ProjectItem("111", "test");
            projectMgr.Add(projectItem);

            //Act
            bool result = projectMgr.ValidateId("*");

            //Assess
            Assert.AreEqual(true, result);
        }
    }
}