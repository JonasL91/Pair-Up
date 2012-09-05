using Models.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ModelsTest
{
    
    
    /// <summary>
    ///This is a test class for GameTest and is intended
    ///to contain all GameTest Unit Tests
    ///</summary>
    [TestClass()]
    public class GameTest
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
        ///A test for Game Constructor
        ///</summary>
        [TestMethod()]
        public void GameConstructorTest()
        {
            Player wPlayer = null; // TODO: Initialize to an appropriate value
            Player bPlayer = null; // TODO: Initialize to an appropriate value
            Game target = new Game(wPlayer, bPlayer);
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        /// <summary>
        ///A test for Game Constructor
        ///</summary>
        [TestMethod()]
        public void GameConstructorTest1()
        {
            Game target = new Game();
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        /// <summary>
        ///A test for AddPointsToPlayers
        ///</summary>
        [TestMethod()]
        [DeploymentItem("PairUpModels.dll")]
        public void AddPointsToPlayersTest()
        {
            Game_Accessor target = new Game_Accessor(); // TODO: Initialize to an appropriate value
            int result = 0; // TODO: Initialize to an appropriate value
            target.AddPointsToPlayers(result);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for RemovePointsFromPlayers
        ///</summary>
        [TestMethod()]
        [DeploymentItem("PairUpModels.dll")]
        public void RemovePointsFromPlayersTest()
        {
            Game_Accessor target = new Game_Accessor(); // TODO: Initialize to an appropriate value
            int result = 0; // TODO: Initialize to an appropriate value
            target.RemovePointsFromPlayers(result);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for BlackPlayer
        ///</summary>
        [TestMethod()]
        public void BlackPlayerTest()
        {
            Game target = new Game(); // TODO: Initialize to an appropriate value
            Player expected = null; // TODO: Initialize to an appropriate value
            Player actual;
            target.BlackPlayer = expected;
            actual = target.BlackPlayer;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for DateOfResult
        ///</summary>
        [TestMethod()]
        public void DateOfResultTest()
        {
            Game target = new Game(); // TODO: Initialize to an appropriate value
            DateTime expected = new DateTime(); // TODO: Initialize to an appropriate value
            DateTime actual;
            target.DateOfResult = expected;
            actual = target.DateOfResult;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for IsInProgress
        ///</summary>
        [TestMethod()]
        public void IsInProgressTest()
        {
            Game target = new Game(); // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            target.IsInProgress = expected;
            actual = target.IsInProgress;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Result
        ///</summary>
        [TestMethod()]
        public void ResultTest()
        {
            Game target = new Game(); // TODO: Initialize to an appropriate value
            double expected = 0F; // TODO: Initialize to an appropriate value
            double actual;
            target.Result = expected;
            actual = target.Result;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for RoundNumber
        ///</summary>
        [TestMethod()]
        public void RoundNumberTest()
        {
            Game target = new Game(); // TODO: Initialize to an appropriate value
            int expected = 0; // TODO: Initialize to an appropriate value
            int actual;
            target.RoundNumber = expected;
            actual = target.RoundNumber;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for WhitePlayer
        ///</summary>
        [TestMethod()]
        public void WhitePlayerTest()
        {
            Game target = new Game(); // TODO: Initialize to an appropriate value
            Player expected = null; // TODO: Initialize to an appropriate value
            Player actual;
            target.WhitePlayer = expected;
            actual = target.WhitePlayer;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}
