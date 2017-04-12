using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OsuTournamentBot.Match_Interaction
{
    public class Team
    {
        private int countPlayers;



        int TotalPlayers;
        string[] TeamsAndPlayers;
        private Player[] players;
        int index;
        public Team(int index, int TotalPlayers, string[] TeamsAndPlayers)
        {
            this.index = index;
            this.TotalPlayers = TotalPlayers;
            this.TeamsAndPlayers = TeamsAndPlayers;
        }

        private string[] Players_nicks = { "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "" };




        public void readPlayers(int index)
        {
            string helpString;
            int PlayersCount;
            if (index == 0)
            {
                helpString = TeamsAndPlayers[1 + index];
                helpString = helpString.Remove(0, 21);
                helpString = helpString.Remove(1, 51);
                PlayersCount = int.Parse(helpString);
                for (int j = 0; j < PlayersCount; j++)
                {
                    Players_nicks[j] = TeamsAndPlayers[3];
                    countPlayers++;
                }
            }
            else
            {
                helpString = TeamsAndPlayers[(1 + (index * 3) + TotalPlayers)];
                helpString = helpString.Remove(0, 21);
                helpString = helpString.Remove(1, 51);
                PlayersCount = int.Parse(helpString);

                for (int j = 0; j < PlayersCount; j++)
                {
                    helpString = TeamsAndPlayers[3 + index * 3 + TotalPlayers];
                    Players_nicks[j] = helpString;
                    countPlayers++;
                    TotalPlayers++;
                }
            }
            players = new Player[PlayersCount];
            for (int i = 0; i < PlayersCount; i++)
            {
                if (i == 0)
                {
                    players[i] = new Player(Players_nicks[i], true, i + 1);
                    Console.WriteLine(players[i].GetName() + " " + players[i].GetNumber() + "   is captain -" + players[i].GetCaptain());
                }
                else
                {
                    players[i] = new Player(Players_nicks[i], false, i + 1);
                    Console.WriteLine(players[i].GetName() + " " + players[i].GetNumber() + "   is captain -" + players[i].GetCaptain());
                }
            }
        }

        public Player[] GetPlayer()
        {
            return players;
        }

        public int GetPlayersCount()
        {
            return countPlayers;
        }


        public void SetPlayersCount(int countPlayers)
        {
            this.countPlayers = countPlayers;
        }

        public string[] GetPlayersNicks()
        {
            return Players_nicks;
        }

        public void SetPlayersNicks(string Players_nick, int index)
        {
            Players_nicks[index] = Players_nick;
        }
    }
}

