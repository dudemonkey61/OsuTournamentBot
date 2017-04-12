using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OsuTournamentBot.DataObjects;
using OsuTournamentBot.Match_Interaction;
using System.IO;

namespace OsuTournamentBot
{
    public class Program
    {


        public static void Main(string[] args)
        {

            //var readApiKey = File.ReadAllLines("DontLetPeopleSeeThis.txt").Take(1).First();
            //OsuAPI theApi = new OsuAPI(readApiKey);
            //MultiMatch multiMatch = theApi.GetMatch(112121);

            string ircUserName = File.ReadAllLines("DontLetPeopleSeeThis.txt").Skip(1).Take(1).First();
            string ircAuthKey = File.ReadAllLines("DontLetPeopleSeeThis.txt").Skip(2).Take(1).First();

            string[] alphaTester = new string[] { "OshieteKudasai", "[_Yui_]", "AlexDark69" };


            IrcClient mp1 = new IrcClient("cho.ppy.sh", 6667, ircUserName, ircAuthKey);
            Lobby_Setup lobby_1_a = new Lobby_Setup(mp1);
            WarmUp_Ban_Setup lobby1_b = new WarmUp_Ban_Setup(lobby_1_a);
            System.Threading.Thread.Sleep(500);
            //mp1.joinRoom("osu");
            //mp1.joinRoom("mp_31931935");




            while (true)
            {
                string message = mp1.readMessage();
                if (message == null)
                {
                    do
                    {
                        mp1.connect();
                        mp1.joinRoom("osu");
                        message = mp1.readMessage();
                    } while (message == null);

                }
                //In case bot were to disconnect, it will try to connect again for eternity


                if (message == "PING cho.ppy.sh")
                {
                    mp1.sendIrcMessage("PONG cho.ppy.sh");
                }
                //PING PONG feature (timeout prevention)


                if (mp1.tcpClient.Connected)
                {
                    if (!message.Contains("!cho@ppy.sh QUIT") &&

                        !message.Contains("!cho@ppy.sh JOIN") &&

                        !message.Contains("!cho@ppy.sh PART") &&

                        !message.Contains(":cho.ppy.sh 353")

                        )
                    {
                        Console.WriteLine(message);
                        if (message == "PING cho.ppy.sh")
                        {
                            Console.WriteLine("PONG cho.ppy.sh");
                        }
                    }
                }
                //Main function for reading messages from IRC / it will omit useles part from cho.ppy.sh (QUIT/JOIN/PART)
                // + it should write : PING - PONG msg from and to server

                if (message.Contains("!;mp make"))
                {
                    for (int i = 0; i < alphaTester.Length; i++)
                    {
                        if (message.Contains(alphaTester[i]))
                        {
                            lobby_1_a.makeLobby(message);
                            break;
                        }
                    }
                }

                if ((message.Contains(lobby_1_a.GetMatchId()) || isAdmin(message)) && message.Contains("!;"))
                {
                    if (message.Contains("!; join"))
                    {
                        mp1.joinRoom(message.Remove(0, message.IndexOf("join ") + 5));
                    }
                    if ((message.Contains("!; Start") || message.Contains("!; start")) && ((lobby_1_a.getSize() == lobby_1_a.GetSlot())|| isAdmin(message)))
                    {
                        lobby1_b.askForWarmUpLink();
                    }

                    for (int i = 0; i < lobby_1_a.countTeams; i++)
                    {
                        for (int j = 0; j < lobby_1_a.Teams[i].GetPlayersCount(); j++)
                        {
                            if (

                                (
                                message.Contains(@"https://osu.ppy.sh/s/")
                                ||
                                message.Contains(@"https://osu.ppy.sh/b/")
                                )

                                &&

                                (
                                message.Contains(lobby_1_a.Teams[i].GetPlayersNicks()[j])
                                &&
                                lobby_1_a.Teams[i].GetPlayer()[j].GetCaptain()
                                )

                              )

                            {
                                lobby1_b.setWarmUpLink(message);
                                lobby1_b.pickWarmUpMap();
                            }

                            if (message.Contains("!; map start") && ((lobby_1_a.Teams[i].GetPlayer()[j].GetCaptain() && message.Contains(lobby_1_a.Teams[i].GetPlayer()[j].GetName())))||isAdmin(message))
                            {
                                lobby1_b.startMap();
                            }

                        }
                    }

                        if (message.Contains("!;Contestants") && isAdmin(message))
                        {
                            lobby_1_a.readMatchContestants();
                            lobby_1_a.writeMatchContestants();
                        }



                        if (!message.Contains("Do you want to be moved into") && (message.Contains("!; 1") || message.Contains("!; 2")))
                        {
                            for (int l = 0; l < lobby_1_a.countTeams; l++)
                            {
                                for (int j = 0; j < lobby_1_a.Teams[l].GetPlayersCount(); j++)
                                {
                                    if (
                                        message.Contains(lobby_1_a.Teams[l].GetPlayersNicks()[j])

                                        &&

                                        ((!message.Contains("Do you want to be moved into") &&

                                        (message.Contains("!; 1") ||
                                        message.Contains("!; yes") ||
                                        message.Contains("!;1") ||
                                        message.Contains("!;no")
                                        ) &&

                                        lobby_1_a.Teams[l].GetPlayer()[l].GetInvDenied() == false)

                                        ))
                                    {
                                        lobby_1_a.movePlayer(lobby_1_a.Teams[l].GetPlayer()[j].GetName(), lobby_1_a.GetSlot());
                                        lobby_1_a.Teams[l].GetPlayer()[j].SetSlot(lobby_1_a.GetSlot());
                                        lobby_1_a.SetSlot(lobby_1_a.GetSlot() + 1);

                                    }
                                    if (
                                        message.Contains(lobby_1_a.Teams[l].GetPlayersNicks()[j])

                                        &&

                                        ((!message.Contains("Do you want to be moved into") &&

                                        (
                                        message.Contains("!; 2") ||
                                        message.Contains("!; no") ||
                                        message.Contains("!;2") ||
                                        message.Contains("!;no")
                                        )

                                        ))

                                        )
                                    {
                                        lobby_1_a.Teams[l].GetPlayer()[j].SetInvDenied(true);
                                    }
                                }
                            }
                        }

                    }


                    if (isAdmin(message) && (message.Contains("!; DIE") || message.Contains("!; die")))
                    {
                        break;
                    }

                }
            }
        



        static bool isAdmin(string message)
        {
            if ((message.Contains(":OshieteKudasai!cho@ppy.sh") || message.Contains(":[_Yui_]!cho@ppy.sh")))
            {
                return true;
            }

            else
            {
                return false;
            }
        }

    }
}

