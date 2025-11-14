using ConsoleApp2._2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppTest2._2
{
    [TestClass]
    public class EmTest
    {
        private Em _em;
        private List<Game> _testAllGames;


        [TestInitialize]
        public void Setup()
        {

            _testAllGames = new List<Game>
            {
                new Game(
                    DateTime.Now,
                    GameType.FinalRound,
                    "Stadium A",
                    new Team("Team A", new List<Player>()),
                    new Team("Team B", new List<Player>()),
                    12,
                    33,
                    new StaffMember[]
                    {
                        new StaffMember { Referee = new Referee { Name = "Referee One", PersonalNumber = 1 }, Role = RefereeRole.MainReferee },
                        new StaffMember { Referee = new Referee { Name = "Referee Two", PersonalNumber = 2 }, Role = RefereeRole.AssistantReferee1 }
                    },
                    new List<IncidentDecision>() 
                    { 
                        new IncidentDecision { Description = "Offside decision", isWrongDecision = true, DecisionTime = DateTime.Now } 
                    }
                ),
                new Game(
                    DateTime.Now.AddDays(-3),
                    GameType.GroupStage,
                    "Stadium B",
                    new Team("Team C", new List<Player>()),
                    new Team("Team D", new List<Player>()),
                    22,
                    11,
                    new StaffMember[]
                    {
                        new StaffMember { Referee = new Referee { Name = "Referee Three", PersonalNumber = 3 }, Role = RefereeRole.MainReferee },
                        new StaffMember { Referee = new Referee { Name = "Referee One", PersonalNumber = 1 }, Role = RefereeRole.AssistantReferee1 }
                    },
                    new List<IncidentDecision>()
                    {
                        new IncidentDecision { Description = "Offside decision", isWrongDecision = true, DecisionTime = DateTime.Now },
                        new IncidentDecision { Description = "Foul play", isWrongDecision = true, DecisionTime = DateTime.Now }
                    }
                )
            };

            _em = new Em(_testAllGames);
        }

        [TestMethod]
        [DataRow("Referee One", 1)]
        public void TestGetGames_WithSpecificReferee(string name, int personaleNumber)
        {
            // Arrange
            var testReferee = new Referee { Name = name, PersonalNumber = personaleNumber};

            // Act
            var searchedReferee = _em.GetGamesWithSpecificReferee(testReferee);

            // Asssert
            Assert.IsTrue(searchedReferee.Exists(game => game.Referees.Any
                (staff => staff.Referee.Name == testReferee.Name
                    && staff.Referee.PersonalNumber == testReferee.PersonalNumber)));

        }

        [TestMethod]
        [DataRow("Referee five", 5)]
        public void TestGetGames_WithSpecificReferee_IfNotFound(string name, int personaleNumber)
        {
            // Arrange
            var testReferee = new Referee { Name = name, PersonalNumber = personaleNumber };

            // Act
            var searchedReferee = _em.GetGamesWithSpecificReferee(testReferee);

            // Asssert

            Assert.IsFalse(searchedReferee.Exists(game => game.Referees.Any
                (staff => staff.Referee.Name == testReferee.Name
                    && staff.Referee.PersonalNumber == testReferee.PersonalNumber)));

        }



        [TestMethod]
        public void TestGetAmountOfTotalIncorrectDecisions_FromAllGames()
        {
            // Act & Assert
            Assert.AreEqual(2, _em.GetAmountOfTotalIncorrectDecisionsFromAllGames(_testAllGames));

        }

        [TestMethod]
        public void TestGetAmountOfTotalIncorrectDecisions_FromSpecificGame()
        {
            // Act & Assert
            Assert.AreEqual(0, _em.GetAmountOfTotalIncorrectDecisionsFromSpecificGame(_testAllGames[0]));
        }

    }
}
