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
            OsuAPI theApi = new OsuAPI("b69187236419834n7612hjwe89r6e98vqwmner6qwe786");

            //The value in the function is made up for security reasons
            MultiMatch multiMatch = theApi.GetMatch(15726351);

            Console.WriteLine(multiMatch.match.match_id);
            Console.WriteLine(multiMatch.match.name);
            Console.WriteLine(multiMatch.match.start_time.ToString());
        }
    }
}
