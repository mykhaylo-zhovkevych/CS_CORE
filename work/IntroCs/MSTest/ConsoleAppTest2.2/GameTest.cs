using ConsoleApp2._2;

namespace ConsoleAppTest2._2
{
    [TestClass]
    public class GameTest
    {

        private List<Player> _allPlayers;
        private List<Team> _allTeams;

        [TestInitialize]
        public void Setup()
        {
            _allPlayers = new List<Player>
            {
                new Player { Name = "player1", PersonalNumber = 1, Goals = 25 },
                new Player { Name = "player2", PersonalNumber = 2, Goals = 28 },
                new Player { Name = "player3", PersonalNumber = 3, Goals = 30 },
                new Player { Name = "player4", PersonalNumber = 4, Goals = 15 },
                new Player { Name = "player5", PersonalNumber = 5, Goals = 8 },
                new Player { Name = "player6", PersonalNumber = 6, Goals = 5 },
                new Player { Name = "player7", PersonalNumber = 7, Goals = 28 },
                new Player { Name = "player8", PersonalNumber = 8, Goals = 10 },
                new Player { Name = "player9", PersonalNumber = 9, Goals = 45 },
                new Player { Name = "player10", PersonalNumber = 10, Goals = 3 },
            };

            _allTeams = new List<Team>
            {
                new Team("Team 1", new List<Player>
                {
                    new Player { Name = "player1", PersonalNumber = 1, Goals = 25 },
                    new Player { Name = "player2", PersonalNumber = 2, Goals = 28 },
                    new Player { Name = "player3", PersonalNumber = 3, Goals = 30 },

                }),
                new Team("Team 2", new List<Player>
                {
                    new Player { Name = "player4", PersonalNumber = 4, Goals = 45 },
                    new Player { Name = "player5", PersonalNumber = 5, Goals = 3 },
                })
            };
        }

        [TestMethod]
        public void CreateTeams_IfIsCorrect()
        {
            // Arrange & Act
            var teams = Game.CreateTeams(2, _allPlayers);

            // Assert
            Assert.IsTrue(teams.Any(team => team.Players.Count() == 5));
            Assert.IsTrue(teams.Count == 2);
        }

        [TestMethod]
        public void CreateTeams_IfNoPlayers()
        {
            Assert.ThrowsException<ArgumentException>(() => Game.CreateTeams(2, null));
        }

        [TestMethod]
        public void CalculateTeamScore_IfIsCorrect()
        {
            // Arrange
            var team01 = new Team("Team 1", new List<Player>
            {
                new Player { Name = "player2", PersonalNumber = 2, Goals = 15 },
                new Player { Name = "player3", PersonalNumber = 3, Goals = 5 }
            });

            // Assert & Act
            Assert.AreEqual(20, Game.CalculateTeamScore(team01));   

        }

        [TestMethod]
        public void CalculateTeamScore_IfTeamHasNoGoals_OrNegative()
        {

            // Arrange
            var team01 = new Team("Team 1", new List<Player>
            {
                new Player { Name = "player2", PersonalNumber = 2, Goals = 0 },
                new Player { Name = "player3", PersonalNumber = 3, Goals = -3 }
            });

            // Assert & Act
            Assert.AreEqual(-3, Game.CalculateTeamScore(team01));

        }



        [TestMethod]
        public void SelectTwoTeams_IfIsCorrect()
        {
            //Arrange
            var selectedTeams = Game.SelectTwoTeams(_allTeams);

            Team team1 = selectedTeams.Item1;

            var expectedTeamOneName = "Team 1";

            // Assert
            Assert.IsNotNull(selectedTeams.Item1);
            Assert.IsNotNull(selectedTeams.Item2);

            Assert.IsTrue(selectedTeams.Item1 != selectedTeams.Item2);
            Assert.AreEqual(expectedTeamOneName, team1.TeamName); 

        }

        [TestMethod]
        public void SelectTwoTeams_IfLessThanTwoTeams()
        {
            // Assert & Act
            Assert.ThrowsException<ArgumentException>(() => Game.SelectTwoTeams(new List<Team> { new Team("Team 1", new List<Player>()) }));

        }

    }
}
