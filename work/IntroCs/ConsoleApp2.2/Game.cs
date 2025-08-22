namespace ConsoleApp2._2
{
    public class Game
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

            var teams = Game.CreateTeams(2, allPlayers); ;


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
                new IncidentDecision { Description = "Foul play", isWrongDecision = true, DecisionTime = DateTime.Now.AddMinutes(10) }
            };

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

            PrintGameDetails(game);


            var allGames = new List<Game> { game };
      
            Em em = new Em(allGames);

            var gamesWithMax = em.GetGamesWithSpecificReferee(referees[0]);
            Console.WriteLine("Falsche Entscheidungen gesamt: " + em.GetAmountOfTotalIncorrectDecisions(game));


            // Erstens es nützlich weil alle Games ausgibt, zweitens weil es override von game nutzt
            foreach (var g in gamesWithMax)
            {
                Console.WriteLine(g);
   
            }

        }


        public DateTime Date { get; set; }
        public GameType GameType { get; set; }
        public string Place { get; set; }
        public int ScoreTeam1 { get; set; }
        public int ScoreTeam2 { get; set; }
        public Team Team1 { get; set; }
        public Team Team2 { get; set; }
        public StaffMember[] Referees { get; set; }
        public List<IncidentDecision> IncidentDecisions { get; set; }

        public Game(
            DateTime date,
            GameType gameType,
            string place,
            Team team1,
            Team team2,
            int scoreTeam1,
            int scoreTeam2,
            StaffMember[] referees,
            List<IncidentDecision> incidentDecisions)
            {
                Date = date;
                GameType = gameType;
                Place = place;
                Team1 = team1;
                Team2 = team2;
                ScoreTeam1 = scoreTeam1;
                ScoreTeam2 = scoreTeam2;
                Referees = referees;
                IncidentDecisions = incidentDecisions;
           }

        public static int CalculateTeamScore(Team team)
        {
            int score = 0;
            foreach (Player player in team.Players)
            {
                score += player.Goals;
            }
            return score;
        }

        public static (Team, Team) SelectTwoTeams(List<Team> teams)
        {
            Random random = new Random();

            if (teams == null || teams.Count < 2) throw new ArgumentException("The team list must contain at least two teams.");

            int firstIndex = random.Next(teams.Count);
            int secondIndex;

            do
            {
                secondIndex = random.Next(teams.Count);
            } while (secondIndex == firstIndex);

            return (teams[firstIndex], teams[secondIndex]);

        }

        public static List<Team> CreateTeams(int numberOfTeams, List<Player> allPlayers)
        {
            if (numberOfTeams <= 0)
                throw new ArgumentOutOfRangeException();
            if (allPlayers == null || allPlayers.Count == 0)
                throw new ArgumentException("There must be at least some players.");

            var teams = Enumerable.Range(1, numberOfTeams)
                                  .Select(i => new Team($"Team {i}", new List<Player>()))
                                  .ToList();

            Random rnd = new Random();
            // _ ist ein Platzhalter für eine Lambda-Variable.
            var shuffledPlayers = allPlayers.OrderBy(_ => rnd.Next()).ToList();

            // Startet in teams[0] und verteilt die Spieler gleichmässig auf alle Teams
            int idx = 0;
            foreach (var p in shuffledPlayers)
            {
                teams[idx].Players.Add(p);
                // % nutze, damit der Index immer im Bereich der Teams bleibt
                idx = (idx + 1) % numberOfTeams;
            }

            return teams;
        }

        public static void PrintGameDetails(Game game)
        {
            Console.WriteLine("\n" + game.ToString());

            Console.WriteLine("\nReferees:");
            foreach (var r in game.Referees)
            {
                Console.WriteLine($"- {r}");
            }

            Console.WriteLine("Incidents:");
            foreach (var i in game.IncidentDecisions)
            {
                Console.WriteLine($"- {i}");
            }
        }

        public override string ToString()
        {
            return $"Game on {Date} at {Place}: {Team1.TeamName} vs {Team2.TeamName}, Score: {ScoreTeam1}-{ScoreTeam2}";
        }
    }
}
