using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OsuTournamentBot.DataObjects;

namespace OsuTournamentBot
{
    public class Program
    {
        public static void Main(string[] args)
        { 
            //The value in the function is made up for security reasons
            OsuAPI theApi = new OsuAPI("aw4e7we35wawa843754587965fg23756fa976efriuwf");

            //The value in the function is made up for security reasons
            MultiMatch multiMatch = theApi.GetMatch(05197635);

            Console.WriteLine(multiMatch.match.match_id);
            Console.WriteLine(multiMatch.match.name);
            Console.WriteLine(multiMatch.match.start_time.ToString());
        }
    }
}
