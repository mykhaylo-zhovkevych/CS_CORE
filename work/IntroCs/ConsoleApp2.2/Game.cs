namespace ConsoleApp2._2
{
    public class Game
    {
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
