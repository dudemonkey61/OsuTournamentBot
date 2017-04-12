using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OsuTournamentBot.Match_Interaction
{
    public class Player
    {
        private string name;
        private bool captain;
        private int number;
        private bool invDenied = false;
        private int slot;
        public Player(string name, bool captain, int number)
        {
            this.name = name;
            this.captain = captain;
            this.number = number;
        }

        public int GetSlot()
        {
            return slot;
        }

        public void SetSlot(int slot)
        {
            this.slot = slot;
        }

        public string GetName()
        {
            return name;
        }

        public void SetName(string name)
        {
            this.name = name;
        }

        public bool GetCaptain()
        {
            return captain;
        }

        public void SetCaptain(bool captain)
        {
            this.captain = captain;
        }

        public int GetNumber()
        {
            return number;
        }

        public void SetNumber(int number)
        {
            this.number = number;
        }

        public bool GetInvDenied()
        {
            return invDenied;
        }

        public void SetInvDenied(bool invDenied)
        {
            this.invDenied = invDenied;
        }



    }
}
