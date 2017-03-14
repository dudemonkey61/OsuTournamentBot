using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OsuTournamentBot.Match_Interaction
{
    public class Player
    {
        public string name;
        public bool captain;
        public int number;
        public Player(string name, bool captain, int number)
        {
            this.name = name;
            this.captain = captain;
            this.number = number;
        }


    }
}
