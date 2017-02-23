using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OsuTournamentBot.DataObjects;
using System.IO;

namespace OsuTournamentBot
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //The value in the function is made up for security reasons\
            var readApiKey = File.ReadAllText(@"DontLetPeopleSeeThis.txt");
            OsuAPI theApi = new OsuAPI(readApiKey);

            //The value in the function is made up for security reasons
            MultiMatch multiMatch = theApi.GetMatch(05197635);


            IrcClient mp1 = new IrcClient("cho.ppy.sh", 6667, "Irc username", "IRC auth key");
            System.Threading.Thread.Sleep(5000);
            mp1.joinRoom("osu");
            while(true)
            {
                string message = mp1.readMessage();


                    if (message.Contains("triger"))
                    {
                    Console.WriteLine(message);
                    }

            }

        }
    }
}
