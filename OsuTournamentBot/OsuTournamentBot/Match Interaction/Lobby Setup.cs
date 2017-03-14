using OsuTournamentBot.Match_Interaction;
using System;
using System.Collections;
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
        string mpCreator;
        string channelName;
        public int countTeams;
        public List<Team> Teams = new List<Team>();
        public int slot=1;
        

        public Lobby_Setup(IrcClient Irc)
        {
            this.Irc = Irc;
        }

        public void readMatchContestants()
        {
            string[] TeamsAndPlayers = File.ReadAllLines("PlayersAndTeams.txt");
            int totalPlayers = 0;
            string test = TeamsAndPlayers[0];
            test = test.Remove(0, 19);
            test = test.Remove(1, 52);
            countTeams = int.Parse(test);
            for (int i = 0; i < countTeams; i++)
            {
                Teams.Add(new Team(i, totalPlayers, TeamsAndPlayers));
                Teams[i].getPlayers(i);
                totalPlayers += Teams[i].countPlayers;
            }
        }
        public void writeMatchContestants()
        {
            Console.WriteLine("");
            for (int i = 0; i < countTeams; i++)
            {
                Console.WriteLine("Team {0} Players :", i + 1);
                Console.WriteLine();
                for (int j = 0; j < Teams[i].countPlayers; j++)
                {
                    Console.WriteLine("- " + Teams[i].Players_nicks[j]);
                }
                Console.WriteLine();
            }
        }

        public void makeLobby(string message)
        {

            mpName = message.Remove(0, message.IndexOf("!;mp make") + 10).ToString();
            //skiping format of msg sent by irc (:username!cho@...)

            mpCreator = message.Remove(0, 1);
            mpCreator = mpCreator.Remove(mpCreator.IndexOf("!cho@ppy.sh"), mpCreator.Length - mpCreator.IndexOf("!cho@ppy.sh"));
            //Geting mpHost name into string for future use

            Irc.sendPrivMessage("!mp make " + mpName, "BanchoBot");
            //sending request to BanchoBot to make mp lobby with chosen name ^^

            int receiveMsgLoopCount = 0;
            //msg counter for next do() while() loop
            do
            {
                message = Irc.readMessage();
                receiveMsgLoopCount++;

            }
            while
            (!
                (message.Contains
                    (
                    ":BanchoBot!cho@ppy.sh PRIVMSG "
                    ) &&
                message.Contains
                    (
                    ":Created the tournament match"
                    )
                ) || receiveMsgLoopCount == 25
             );
            //Looping thorugh 25 messages until it were to find MSG from BanchoBot (to get msg containing lobby data)

            if (message.Contains(
                ":BanchoBot!cho@ppy.sh PRIVMSG ") &&
                message.Contains(
                ":Created the tournament match"
                ))
            //geting the msg from BanchoBot containing mp id

            {
                matchLink = message.Remove(0, message.IndexOf("https"));
                matchLink = matchLink.Remove(matchLink.IndexOf("mp") + 11, matchLink.Length - matchLink.IndexOf("mp") - 11);
                //geting the 8 char mp id from msg sent by BanchoBot

                matchId = matchLink.Remove(0, 22);
                //geting the 8 char mp id from msg sent by BanchoBot


                Console.WriteLine("------------------------");

                Console.WriteLine("Lobby name =" + @"{0}", mpName);
                Console.WriteLine("Lobby Creator =" + @"{0}", mpCreator);
                Console.WriteLine("Lobby Link ={0}", matchLink);
                Console.WriteLine("Lobby Id ={0}", matchId);

                Console.WriteLine("------------------------");
                //Console Log (just for now, to get if everything is alright)

                Irc.sendPrivMessage("Lobby Name =" + @mpName, mpCreator);
                Irc.sendPrivMessage("Lobby Host =" + @mpCreator, mpCreator);
                Irc.sendPrivMessage("Lobby Link =" + matchLink, mpCreator);
                Irc.sendPrivMessage("Lobby Id   =" + matchId, mpCreator);
                //Priv msg for "Host" - person who made lobby

                channelName = "mp_" + matchId;
                Irc.joinRoom(channelName);
                //Joining match lobby by bot

                string[] lobbySettings = File.ReadAllLines("Match Settings.txt");
                string scoremode, teammode, size;
                //variables for "Match Settings.txt"

                if (lobbySettings[0].Remove(0, 6) != @"// 1-16")
                {
                    size = lobbySettings[0];
                    size = size.Remove(0, 5);
                    size = size.Remove(2, size.Length - 2);
                }
                else
                {
                    size = lobbySettings[0];
                    size = size.Remove(0, 5);
                    size = size.Remove(1, size.Length - 1);
                }
                //Geting size from "Match Settings.txt"

                teammode = lobbySettings[1];
                teammode = teammode.Remove(0, 9);
                teammode = teammode.Remove(1, teammode.Length - 1);
                //Geting teammode from "Match Settings.txt"

                scoremode = lobbySettings[2];
                scoremode = scoremode.Remove(0, 10);
                scoremode = scoremode.Remove(1, scoremode.Length - 1);
                //Geting scoremode from Match Settings.txt file

                Console.WriteLine(size + " " + teammode + " " + scoremode);
                //Console Log (for now) for checking data from "Match Settings.txt"

                Irc.sendChatMessage
                    ("!mp set " +
                    teammode +
                    " " +
                    scoremode +
                    " " +
                    size,
                    channelName);
                //Sending MSG to lobby to change it's settings to the ones specified in "Match Settings.txt"




                Irc.sendChatMessage("!mp move " + mpCreator + " 1", "mp_" + matchId);
                //Forcefully moving the Creator to lobby


                Irc.sendChatMessage("!mp host " + mpCreator, "mp_" + matchId);
                //Giving Host to the lobby Creator

                invitePlayers();
            }
        }
        public void invitePlayers()
        {
            for (int i = 0; i < countTeams; i++)
            {
                for (int j = 0; j < Teams[i].countPlayers; j++)
                {
                    Irc.sendPrivMessage("Do you want to be moved into " + mpName + " lobby? : {!; 1} - yes | {!; 2} - no", Teams[i].Players_nicks[j]);
                }
            }
        }

        public void movePlayer(string nick,int slot)
        {
            Irc.sendChatMessage("!mp move " + nick + " " + slot, "#mp_" + matchId);
        }


    }
}

