using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OsuTournamentBot.Match_Interaction
{
    class WarmUp_Ban_Setup
    {
        private string WarmUpLink;

        private Lobby_Setup lobby1;
        

        public WarmUp_Ban_Setup(Lobby_Setup lobby1)
        {
            this.lobby1 = lobby1;
        }

        public void setWarmUpLink(string WarmUpLink)
        {
            if (WarmUpLink.Contains(@"/s/"))
            {
                lobby1.GetIrcClient().sendLobbyMessage("Please use map link for specific Diff, the link should contain /b/ in it", lobby1.GetMatchId());
            }

            else
            {
                WarmUpLink = WarmUpLink.Remove(0, WarmUpLink.IndexOf(@"https://osu.ppy.sh/b/"));
                this.WarmUpLink = WarmUpLink;
            }
        }

        public void setLobbyMap()
        {
            lobby1.GetIrcClient().sendLobbyMessage("!mp map " + WarmUpLink, lobby1.GetMatchId());
            lobby1.GetIrcClient().sendLobbyMessage("Please write !; map start to start map", lobby1.GetMatchId());
        }

        public void pickWarmUpMap()
        {
            if (WarmUpLink != "")
            {
                lobby1.GetIrcClient().sendLobbyMessage("!mp map " + getWarmUpID(), lobby1.GetMatchId());
            }

            else
            {
                lobby1.GetIrcClient().sendLobbyMessage("Please pick map first", lobby1.GetMatchId());
            }
        }

        public string getWarmUpLink()
        {
            return WarmUpLink;
        }

        public string getWarmUpID()
        {
            string helpstring= WarmUpLink.Remove(0, WarmUpLink.IndexOf("/b/") + 3);
            helpstring=helpstring.Remove(helpstring.IndexOf("&m"), 4);
            return helpstring;
            
        }



        public void askForWarmUpLink()
        {
            lobby1.GetIrcClient().sendLobbyMessage("Please input link for warmup map :)", lobby1.GetMatchId());
        }

        public void startMap()
        {
            lobby1.GetIrcClient().sendLobbyMessage("!mp start 10", lobby1.GetMatchId());
        }

        


    }
}
