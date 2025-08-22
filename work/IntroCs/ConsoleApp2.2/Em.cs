using System.Collections;
using System.Collections.Generic;

namespace ConsoleApp2._2
{
    public class Em
    {
        private List<Game> allGames;

        public Em(List<Game> games)
        {
            allGames = games;
        }

        public List<Game> GetGamesWithSpecificReferee(Referee referee)
        {
            if (allGames == null || referee == null)
                return new List<Game>();

            return allGames.FindAll(game =>
                game.Referees.Any(staff =>
                    staff.Referee.Name == referee.Name &&
                    staff.Referee.PersonalNumber == referee.PersonalNumber
                )
            );
        }


        public int GetAmountOfTotalIncorrectDecisions(Game game)
        {
            if ((DateTime.Now - game.Date).TotalHours >= 48)
            {
                return allGames.Sum(g => g.IncidentDecisions.Count(IncidentDecision => IncidentDecision.isWrongDecision));
            }
            return 0;
        }
    }
}
