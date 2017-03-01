using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OsuTournamentBot.Match_Interaction
{
    class Lobby_Setup
    {
        IrcClient Irc;
        string matchId;
        string matchLink;
        string mpName;
        string mpHost;

        public Lobby_Setup(IrcClient Irc)
        {
            this.Irc = Irc;
        }

        public void makeLobby(string message)
        {

            mpName = message.Remove(0,message.IndexOf("!;mp make") + 10).ToString();
            //skiping format of msg sent by irc (:username!cho@...)

            mpHost = message.Remove(0, 1);
            mpHost = mpHost.Remove(mpHost.IndexOf("!cho@ppy.sh"), mpHost.Length - mpHost.IndexOf("!cho@ppy.sh"));

            //Geting mpHost name into string for future use

            Irc.sendPrivMessage("!mp make " + mpName, "BanchoBot");
            //sending request to BanchoBot to make mp lobby with chosen name ^^
            while (true)
            {
                message = Irc.readMessage();
                if (message.Contains(
                ":BanchoBot!cho@ppy.sh PRIVMSG ") &&
                message.Contains(
                ":Created the tournament match"
                )) { break; }
            }
            if (message.Contains(
                ":BanchoBot!cho@ppy.sh PRIVMSG ") &&
                message.Contains(
                ":Created the tournament match"
                ))
            //geting the msg from BanchoBot containing mp id

            {
                matchLink = message.Remove(0, message.IndexOf("https"));
                matchLink = matchLink.Remove(matchLink.IndexOf("mp") + 11, matchLink.Length-matchLink.IndexOf("mp")-11);
                //geting the 8 char mp id from msg sent by BanchoBot

                matchId = matchLink.Remove(0,22);
                //geting the 8 char mp id from msg sent by BanchoBot
                Console.WriteLine("");
                Console.WriteLine("Lobby name ={0}",mpName);
                Console.WriteLine("Lobby Host ={0}",mpHost);
                Console.WriteLine("Lobby Link ={0}",matchLink);
                Console.WriteLine("Lobby Id ={0}",matchId);
                Console.WriteLine("");

                Irc.sendPrivMessage("Lobby name ="+ mpName, mpHost);
                Irc.sendPrivMessage("Lobby Host =" + mpHost, mpHost);
                Irc.sendPrivMessage("Lobby Link ="+ matchLink, mpHost);
                Irc.sendPrivMessage("Lobby Id =" + matchId, mpHost);

                Irc.joinRoom("mp_" + matchId);





                //Irc.sendChatMessage("!mp move " + mpHost + " 1", "mp_" + matchId);
                //System.Threading.Thread.Sleep(100);
                //Irc.sendChatMessage("!mp host " + mpHost, "mp_" + matchId);
            }
            

        }
    }
}
