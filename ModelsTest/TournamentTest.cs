using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models.Domain;
using System.Collections.ObjectModel;

namespace ModelsTest
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class TournamentTest
    {
        private Tournament Tournament { get; set; }
        public TournamentTest()
        {
           

        }

        [TestInitialize]
        public void Initialize()
        {
            DummyContext context = new DummyContext();
            Tournament = context.Tournament;
        }

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
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        //It should not be possible to add an empty player to a tournament.
        [TestMethod]
        public void TestAddEmptyPlayerToTournament()
        {
            Player player = new Player();
            Tournament.AddPlayer(player);
            Assert.IsFalse(Tournament.Players.Contains(player));
        }

        [TestMethod]
        public void TestAddValidPlayerToTournament()
        {
            Player player = new Player("Pol","De Bakker");
            Tournament.AddPlayer(player);
            Assert.IsTrue(Tournament.Players.Contains(player));
        }

        //It should not be possible to add a player with a first name and a last name that already exist in the tournament.µ
        //Testmethod returns false, however the private method in Tournament: DoesPlayerExistsInTournament, does work.
        //To make this method work, we should either make the method public and test immediatly that method. Or we could change the method GetHashCode for the Player class.
        /*[TestMethod]
        public void TestAddPlayerAlreadyExistsToTournament()
        {
            Player player = new Player("Jonas", "Larsen");
            Tournament.AddPlayer(player);
            Assert.IsFalse(Tournament.Players.Any(p => player.GetHashCode() == p.GetHashCode()));
        }
         **/
        [TestMethod]
        public void PairUpFirstRound()
        {
            
        }

        /// <summary>
        ///A test for Tournament Constructor
        ///</summary>
        [TestMethod()]
        public void TournamentConstructorTest()
        {
            Tournament target = new Tournament();
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        /// <summary>
        ///A test for AddPlayer
        ///</summary>
        [TestMethod()]
        public void AddPlayerTest()
        {
            Tournament target = new Tournament(); // TODO: Initialize to an appropriate value
            Player player = null; // TODO: Initialize to an appropriate value
            target.AddPlayer(player);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for CanPlayAgainstPlayer
        ///</summary>
        [TestMethod()]
        [DeploymentItem("PairUpModels.dll")]
        public void CanPlayAgainstPlayerTest()
        {
            Tournament_Accessor target = new Tournament_Accessor(); // TODO: Initialize to an appropriate value
            Player worstInRank = null; // TODO: Initialize to an appropriate value
            Player betterInRank = null; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.CanPlayAgainstPlayer(worstInRank, betterInRank);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for DeterminePlayerColor
        ///</summary>
        [TestMethod()]
        [DeploymentItem("PairUpModels.dll")]
        public void DeterminePlayerColorTest()
        {
            Tournament_Accessor target = new Tournament_Accessor(); // TODO: Initialize to an appropriate value
            Player playerA = null; // TODO: Initialize to an appropriate value
            Player playerB = null; // TODO: Initialize to an appropriate value
            Player[] expected = null; // TODO: Initialize to an appropriate value
            Player[] actual;
            actual = target.DeterminePlayerColor(playerA, playerB);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for DoesPlayerExistInTournament
        ///</summary>
        [TestMethod()]
        public void DoesPlayerExistInTournamentTest()
        {
            Tournament target = new Tournament(); // TODO: Initialize to an appropriate value
            Player player = null; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.DoesPlayerExistInTournament(player);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

      

        /// <summary>
        ///A test for PairUp
        ///</summary>
        [TestMethod()]
        public void PairUpTest()
        {
            Tournament target = new Tournament(); // TODO: Initialize to an appropriate value
            target.PairUp();
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

      

        /// <summary>
        ///A test for Name
        ///</summary>
        [TestMethod()]
        public void NameTest()
        {
            Tournament target = new Tournament(); // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            target.Name = expected;
            actual = target.Name;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Players
        ///</summary>
        [TestMethod()]
        public void PlayersTest()
        {
            Tournament target = new Tournament(); // TODO: Initialize to an appropriate value
            List<Player> expected = null; // TODO: Initialize to an appropriate value
            List<Player> actual;
            target.Players = expected;
            actual = target.Players;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}
