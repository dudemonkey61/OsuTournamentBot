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
        public int countPlayers;
        int TotalPlayers;
        string[] TeamsAndPlayers;

        int index;
        public Team(int index, int TotalPlayers,string[] TeamsAndPlayers)
        {
            this.index = index;
            this.TotalPlayers = TotalPlayers;
            this.TeamsAndPlayers = TeamsAndPlayers;
        }

        public string[] Players_nicks= {"", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "" };
        
        public void getPlayers(int index)
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
            Player[] players = new Player[PlayersCount];
            for (int i = 0; i < PlayersCount; i++)
            {
                if (i == 0)
                {
                    players[i] = new Player(Players_nicks[i], true, i+1);
                    Console.WriteLine(players[i].name + " " + players[i].number + " " + players[i].captain);
                }
                else
                {
                    players[i] = new Player(Players_nicks[i], false, i+1);
                    Console.WriteLine(players[i].name + " " + players[i].number + " " + players[i].captain);
                }
            }
        }
    }
}
