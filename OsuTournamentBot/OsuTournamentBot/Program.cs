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
            //var readApiKey = File.ReadAllLines("DontLetPeopleSeeThis.txt").Take(1).First();
            string ircUserName = File.ReadAllLines("DontLetPeopleSeeThis.txt").Skip(1).Take(1).First();
            string ircAuthKey = File.ReadAllLines("DontLetPeopleSeeThis.txt").Skip(2).Take(1).First();
            //OsuAPI theApi = new OsuAPI(readApiKey);

            //The value in the function is made up for security reasons
            //MultiMatch multiMatch = theApi.GetMatch(112121);




            IrcClient mp1 = new IrcClient("cho.ppy.sh", 6667, ircUserName, ircAuthKey);
            System.Threading.Thread.Sleep(5000);
            mp1.joinRoom("osu");
            string matchId="";
            while (true)
            {
                bool manualInputInConsole = false;
                string message = mp1.readMessage();

                if (!mp1.tcpClient.Connected)
                {
                    mp1.connect();
                }
                if (manualInputInConsole == true)
                {
                    switch (Console.ReadLine())
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
                    }
                    //manualInputInConsole = false;
                }

                if (!message.Contains("!cho@ppy.sh QUIT") && !message.Contains("!cho@ppy.sh JOIN") && !message.Contains("+"+ircUserName))
                {
                    Console.WriteLine(message);
                }

                if (message.Contains("!;"))
                {
                    manualInputInConsole = true;
                }
                if (message.Contains("!mp make")&&message.Contains("[_Yui_]"))
                {
                    string mpName = message.Skip(
                        message.IndexOf("!mp make" + 8)
                        ).ToString();//skiping format of msg sent by irc (:username!cho@...)

                    mp1.sendPrivMessage(
                        "!mp make " + mpName, "BanchoBot"
                        );//sending request to BanchoBot to make mp lobby with chosen name ^^

                    if (message.Contains(
                        ":BanchoBot!cho@ppy.sh PRIVMSG " +
                        ircUserName + 
                        ":Created the tournament match"
                        ))//geting the msg from BanchoBot containing mp id
                    {
                        matchId = message.Skip(
                            message.IndexOf("/mp/"+4)
                            ).Take(8).ToString();//geting the 8 char mp id from msg sent by BanchoBot
                        Console.WriteLine(matchId);
                    }
                }





                //if (message.Contains("!;"))
                //    {
                //    mp1.sendPrivMessage();
                //    Console.WriteLine(message);
                //}


            }
            

            
        }
        
    }
}
