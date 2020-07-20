using System;
using System.Net.Sockets;
using System.IO;

namespace Stream_AFK_Text_Game
{
    static class Twitch
    {
        public static string DisplayName;
        public static string UserName;
        public static string AuthKey;

        private static TcpClient TwitchClient;
        private static StreamReader SReader;
        private static StreamWriter SWriter;

        #region Connect to Twitch

        public static void LaunchConnection()
        {
            GetTwitchDetails();
            ConnectToTwitch();
            var timer = new System.Threading.Timer(e => ReadChat(), null, TimeSpan.Zero, TimeSpan.FromSeconds(1));
        }

        static void GetTwitchDetails()
        {
            Console.WriteLine("Twitch Client: Getting Details...");
            DisplayName = File.ReadAllText("Channel Details\\Display Name.txt");
            UserName = File.ReadAllText("Channel Details\\User Name.txt");
            AuthKey = File.ReadAllText("Channel Details\\Auth Key.txt");
        }

        static void ConnectToTwitch()
        {
            Console.WriteLine("Twitch Client: Connecting...");
            TwitchClient = new TcpClient("irc.chat.twitch.tv", 6667);
            SReader = new StreamReader(TwitchClient.GetStream());
            SWriter = new StreamWriter(TwitchClient.GetStream());
            SWriter.WriteLine("PASS " + AuthKey);
            SWriter.WriteLine("NICK " + UserName.ToLower());
            SWriter.WriteLine("USER " + UserName + " 8 * :" + UserName);
            SWriter.WriteLine("JOIN #" + DisplayName.ToLower());
            SWriter.Flush();
            string Response = SReader.ReadLine();
            if (Response.Contains("Welcome, GLHF"))
            {
                Console.WriteLine("Twitch Client: Connected");
            }
            else
            {
                Console.WriteLine("Twitch Client: Failed to Connect");
                return;
            }
            Console.WriteLine(Response);
            string Response2 = SReader.ReadLine();
            Console.WriteLine(Response2);
        }

        #endregion

        #region Write/Read Chat

        public static void WriteToChat(string Msg)
        {
            SWriter.WriteLine("PRIVMSG #" + DisplayName.ToLower() + " :" + Msg);
        }

        static void ReadChat()
        {
            if (!TwitchClient.Connected)
            {
                ConnectToTwitch();
                return;
            }
            if(TwitchClient.Available > 0)
            {
                var Msg = SReader.ReadLine();
                Console.WriteLine(Msg);
                if(Msg.Contains("PRIVMSG"))
                {
                    var splitPoint = Msg.IndexOf("!", 1);
                    var chatName = Msg.Substring(0, splitPoint);
                    chatName = chatName.Substring(1);
                    splitPoint = Msg.IndexOf(":", 1);
                    Msg = Msg.Substring(splitPoint + 1);
                    if (Msg.Substring(0, 1) == "!" && Msg.Length <= 3)
                        MainClass.ChatOptions.CheckNewVote(Msg);
                }
                else if (Msg.Contains("PING"))
                {
                    SWriter.WriteLine("PONG :tmi.twitch.tv");
                }
            }
        }

        #endregion
    }
}
