using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OsuTournamentBot.DataObjects;

namespace OsuTournamentBot
{
    public class Program
    {
        public static void Main(string[] args)
        { 
            //The value in the function is made up for security reasons
            OsuAPI theApi = new OsuAPI("aw4e7we35wawa843754587965fg23756fa976efriuwf");

            //The value in the function is made up for security reasons
            MultiMatch multiMatch = theApi.GetMatch(05197635);


            IrcClient mp1 = new IrcClient("cho.ppy.sh", 6667, "OshieteKudasai", "3f65aef0");
            mp1.joinPM("[_Yui_]");
            mp1.joinRoom("[_Yui_]");
            while(true)
            {
                string message = mp1.readMessage();
                if(message!=null)
                {
                    mp1.sendIrcMessage("Yo Yo");
                }
            }

        }
    }
}
