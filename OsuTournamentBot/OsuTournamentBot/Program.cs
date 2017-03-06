using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OsuTournamentBot.DataObjects;
using OsuTournamentBot.Match_Interaction;
using System.IO;

namespace OsuTournamentBot
{
    public class Program
    {


        public static void Main(string[] args)
        {
            //var readApiKey = File.ReadAllLines("DontLetPeopleSeeThis.txt").Take(1).First();
            //OsuAPI theApi = new OsuAPI(readApiKey);
            //MultiMatch multiMatch = theApi.GetMatch(112121);

            string ircUserName = File.ReadAllLines("DontLetPeopleSeeThis.txt").Skip(1).Take(1).First();
            string ircAuthKey = File.ReadAllLines("DontLetPeopleSeeThis.txt").Skip(2).Take(1).First();

            string[] alphaTester = new string[] { "OshieteKudasai", "[_Yui_]", "AlexDark69" };

            IrcClient mp1 = new IrcClient("cho.ppy.sh", 6667, ircUserName, ircAuthKey);
            Lobby_Setup lobby_1 = new Lobby_Setup(mp1);
            System.Threading.Thread.Sleep(5000);
            //mp1.joinRoom("osu");
            mp1.joinRoom("mp_31578706");
            



            while (true)
            {
                string message = mp1.readMessage();
                if (message == null)
                {
                    do
                    {
                        mp1.connect();
                        mp1.joinRoom("osu");
                        message = mp1.readMessage();
                    } while (message == null);

                }
                //In case bot were to disconnect, it will try to connect again for eternity


                if (message == "PING cho.ppy.sh")
                {
                    mp1.sendIrcMessage("PONG cho.ppy.sh");
                }
                //PING PONG feature (timeout prevention)


                if (mp1.tcpClient.Connected)
                {
                    if (!message.Contains("!cho@ppy.sh QUIT") &&

                        !message.Contains("!cho@ppy.sh JOIN") &&

                        !message.Contains("!cho@ppy.sh PART") &&

                        !message.Contains(":cho.ppy.sh 353")

                        )
                    {
                        Console.WriteLine(message);
                        if (message == "PING cho.ppy.sh")
                        {
                            Console.WriteLine("PONG cho.ppy.sh");
                        }
                    }
                }
                //Main function for reading messages from IRC / it will omit useles part from cho.ppy.sh (QUIT/JOIN/PART)


                if (message.Contains("!;mp make"))
                {
                    for (int i = 0; i < alphaTester.Length; i++)
                    {
                        if (message.Contains(alphaTester[i]))
                        {
                            lobby_1.makeLobby(message);
                            break;
                        }
                    }
                }
                //Experimental "!;mp make" function, using Lobby_Setup.cs 
            }
        }
    }
}
