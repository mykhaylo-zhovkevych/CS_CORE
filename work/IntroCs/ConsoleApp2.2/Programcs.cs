using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2._2
{
    public class Programcs
    {
        public static void Main(string[] args)
        {
            var allPlayers = new List<Player>
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

            Referee[] referees = new Referee[3];

            referees[0] = new Referee { Name = "Max Oliver", PersonalNumber = 101 };
            referees[1] = new Referee { Name = "Anna Schmidt", PersonalNumber = 102 };
            referees[2] = new Referee { Name = "Peter Olsk", PersonalNumber = 103 };

            StaffMember[] staff = new StaffMember[3];

            staff[0] = new StaffMember { Referee = referees[0], Role = RefereeRole.MainReferee };
            staff[1] = new StaffMember { Referee = referees[1], Role = RefereeRole.AssistantReferee1 };
            staff[2] = new StaffMember { Referee = referees[2], Role = RefereeRole.CoachCalmer };


            List<IncidentDecision> incidentDecisions = new List<IncidentDecision>
            {
                new IncidentDecision { Description = "Offside decision", isWrongDecision = false, DecisionTime = DateTime.Now },
                new IncidentDecision { Description = "Foul play", isWrongDecision = true, DecisionTime = DateTime.Now }
            };

            var teams = Game.CreateTeams(2, allPlayers);

            var selectedTeams = Game.SelectTwoTeams(teams);
            // SelectTwoTeams gibt ein TUPEL zurück: (Team, Team)
            // Ein Tupel hat automatische Properties: Item1, Item2...
            Team team1 = selectedTeams.Item1;
            Team team2 = selectedTeams.Item2;

            int scoreTeam1
                = Game.CalculateTeamScore(team1);
            int scoreTeam2
                = Game.CalculateTeamScore(team2);

            Game game = new Game(
                DateTime.Now,
                GameType.GroupStage,
                "Stadium A",
                team1,
                team2,
                scoreTeam1,
                scoreTeam2,
                staff,
                incidentDecisions);

            Game game02 = new Game(
                DateTime.Now.AddDays(-3),
                GameType.GroupStage,
                "Stadium B",
                team1,
                team2,
                scoreTeam1,
                scoreTeam2,
                staff,
                incidentDecisions);

            Game.PrintGameDetails(game);


            var allGames = new List<Game> { game, game02 };

            Em em = new Em(allGames);

            var gamesWithMax = em.GetGamesWithSpecificReferee(referees[0]);
            Console.WriteLine("Falsche Entscheidungen gesamt: " + em.GetAmountOfTotalIncorrectDecisionsFromAllGames(allGames));


            // Erstens es nützlich weil alle Games ausgibt, zweitens weil es override von game nutzt
            foreach (var g in gamesWithMax)
            {
                Console.WriteLine(g);

            }

        }
    }
}
