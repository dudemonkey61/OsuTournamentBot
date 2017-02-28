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
            //var readApiKey = File.ReadAllLines("DontLetPeopleSeeThis.txt").Take(1).First();
            //OsuAPI theApi = new OsuAPI(readApiKey);
            //MultiMatch multiMatch = theApi.GetMatch(112121);

            string ircUserName = File.ReadAllLines("DontLetPeopleSeeThis.txt").Skip(1).Take(1).First();
            string ircAuthKey = File.ReadAllLines("DontLetPeopleSeeThis.txt").Skip(2).Take(1).First();

            string[] alphaTester = new string[] { "OshieteKudasai", "[_Yui_]", "AlexDark69" };

            IrcClient mp1 = new IrcClient("cho.ppy.sh", 6667, ircUserName, ircAuthKey);
            System.Threading.Thread.Sleep(5000);
            mp1.joinRoom("osu");
            string matchId="";
            string matchLink = "";

           
            

            while (true)
            {
                string message = mp1.readMessage();
                if (message==null)
                {

                    mp1.connect();
                    mp1.joinRoom("osu");
                    message = mp1.readMessage();
                    
                }

                if (message=="PING cho.ppy.sh")
                {
                    mp1.sendIrcMessage("PONG cho.ppy.sh");
                }



                

                    /*switch (Console.ReadLine())
                    {
                        case "quit":
                            Environment.Exit(0);
                            break;

                        case "!mp make":
                            Console.Write("Please write the name of the room you want to make -");
                            string mpName = Console.ReadLine();
                            mp1.sendPrivMessage("!mp make " + mpName, "BanchoBot");
                            if (message.Contains(":BanchoBot!cho@ppy.sh PRIVMSG " + ircUserName + ":Created the tournament match"))
                            {
                                matchId = message.Skip(51 + ":BanchoBot!cho@ppy.sh PRIVMSG ".Length).Take(8).ToString();
                                Console.WriteLine(matchId);
                            }
                            break;

                        case "!mp join":
                            mp1.joinRoom("mp_31339974");
                            mp1.sendChatMessage(@"!mp move OshieteKudasai 1", "mp_" + matchId);
                            if (message.Contains(":BanchoBot!cho@ppy.sh PRIVMSG "))
                            {
                                Console.WriteLine(message);
                            }
                            break;

                        case "!PM BanchoBot":

                            mp1.sendPrivMessage("!help", "BanchoBot");
                            if (message.Contains(":BanchoBot!cho@ppy.sh PRIVMSG "))
                            {
                                Console.WriteLine(message);
                            }
                            break;

                        case "ping":
                            Console.WriteLine("pong");
                            break;
                    default:
                        {
                            break;
                        }
                    }*/

                if (mp1.tcpClient.Connected)
                {
                    if (!message.Contains("!cho@ppy.sh QUIT") &&

                        !message.Contains("!cho@ppy.sh JOIN")&&

                        !message.Contains("!cho@ppy.sh PART") &&

                        message.Length<=300

                        )
                    {
                        Console.WriteLine(message);
                    }
                }


                if (message.Contains("!;mp make"))
                {
                    for (int i = 0; i < 3; i++)
                    {
                        if (message.Contains(alphaTester[i]))
                        {
                            //bool matchInProgres = true;

                            string mpName = message.Remove(0,
                                   message.IndexOf("!;mp make") + 10
                                   ).ToString();//skiping format of msg sent by irc (:username!cho@...)

                            string mpHost = message.Remove(0, 1).Remove(message.IndexOf("!cho@ppy.sh"), message.Length).ToString();
                            //Geting mpHost name into string for future use

                            mp1.sendPrivMessage(
                                 "!mp make " + mpName, "BanchoBot"
                                 );//sending request to BanchoBot to make mp lobby with chosen name ^^

                            message = mp1.readMessage();

                            if (message.Contains(
                                ":BanchoBot!cho@ppy.sh PRIVMSG " +
                                ircUserName +
                                ":Created the tournament match"
                                ))//geting the msg from BanchoBot containing mp id

                            {
                                matchLink = message.Remove(0,
                                    message.IndexOf("https")
                                    ).Remove(message.IndexOf("mp/") + 11, message.Length).ToString();//geting the 8 char mp id from msg sent by BanchoBot

                                matchId = matchLink.Remove(
                                    message.IndexOf("/mp/" + 4)
                                    ).Take(8).ToString();//geting the 8 char mp id from msg sent by BanchoBot
                                Console.WriteLine(matchId);
                                Console.WriteLine(matchLink);
                            }



                            mp1.sendPrivMessage(
                                 "!mp move " + mpHost + " 1", "BanchoBot"
                                 );
                            mp1.sendPrivMessage(
                                 "!mp addref " + mpHost, "BanchoBot"
                                 );


                        }
                    }
                }
            } 
        }
    }
}
