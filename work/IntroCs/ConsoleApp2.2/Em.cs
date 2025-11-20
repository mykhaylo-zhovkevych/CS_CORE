using System.Collections;
using System.Collections.Generic;

namespace ConsoleApp2._2
{
    public class Em
    {
        private List<Game> allGames;
        private Game oneGame;

        public Em(List<Game> games)
        {
            allGames = games;
        }

        public List<Game> GetGamesWithSpecificReferee(Referee referee)
        {
            if (allGames == null || referee == null)
            {
                return new List<Game>();
            }
                
            return allGames.FindAll(game =>
                game.Referees.Any(staff =>
                    staff.Referee.Name == referee.Name &&
                    staff.Referee.PersonalNumber == referee.PersonalNumber
                )
            );
        }


        //public int GetAmountOfTotalIncorrectDecisionsFromAllGames(List<Game> allGames)
        //{
        //    foreach (var game in allGames)
        //    {
        //        if ((DateTime.Now - game.Date).TotalHours >= 48) 
        //        {
        //            List<Game> actualGames = new List<Game>();    
        //            actualGames.Add(game);

        //            return actualGames.Sum(g => g.IncidentDecisions.Count(IncidentDecision => IncidentDecision.isWrongDecision));
        //        }
                
        //    }

        //        //int wrongCount = 0;

        //        //foreach (var g in allGames)  
        //        //{
        //        //    foreach (var decision in g.IncidentDecisions)  
        //        //    {
        //        //        if (decision.isWrongDecision)
        //        //        {
        //        //            wrongCount++;
        //        //        }
        //        //    }
        //        //}

        //        //return wrongCount;
            
        //    return 0;
        //}

        public int GetAmountOfTotalIncorrectDecisionsFromSpecificGame(Game game)
        {
            if ((DateTime.Now - game.Date).TotalHours >= 48)
            {
                return game.IncidentDecisions.Count(IncidentDecision => IncidentDecision.isWrongDecision);
            }

            return 0;
        }
    }
}
