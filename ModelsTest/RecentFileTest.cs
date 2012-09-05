using Models.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ModelsTest
{
    
    
    /// <summary>
    ///This is a test class for RecentFileTest and is intended
    ///to contain all RecentFileTest Unit Tests
    ///</summary>
    [TestClass()]
    public class RecentFileTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for RecentFile Constructor
        ///</summary>
        [TestMethod()]
        public void RecentFileConstructorTest()
        {
            string filepath = string.Empty; // TODO: Initialize to an appropriate value
            RecentFile target = new RecentFile(filepath);
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        /// <summary>
        ///A test for ShortenPathname
        ///</summary>
        [TestMethod()]
        public void ShortenPathnameTest()
        {
            string pathname = string.Empty; // TODO: Initialize to an appropriate value
            int maxLength = 0; // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            actual = RecentFile.ShortenPathname(pathname, maxLength);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}
