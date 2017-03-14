using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OsuTournamentBot.Match_Interaction
{
    class WarmUp_Ban_Setup
    {
        List<Team> Teams;
        IrcClient lobby_client;
        public WarmUp_Ban_Setup(List<Team> Teams ,IrcClient lobby_client)
        {
            this.Teams = Teams;
            this.lobby_client = lobby_client;
        }


    }
}
