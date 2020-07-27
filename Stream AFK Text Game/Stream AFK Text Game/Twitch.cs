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

        static int ChatWriterTimer;

        #region Connect to Twitch

        public static void LaunchConnection()
        {
            GetTwitchDetails();
            ConnectToTwitch();
            var timer = new System.Threading.Timer(e => ReadChat(), null, TimeSpan.Zero, TimeSpan.FromSeconds(1));
        }

        static void GetTwitchDetails()
        {
            ConWin.UpdateTwitchLog("Twitch Client: Getting Details...");
            DisplayName = File.ReadAllText("Channel Details\\Display Name.txt");
            UserName = File.ReadAllText("Channel Details\\User Name.txt");
            AuthKey = File.ReadAllText("Channel Details\\Auth Key.txt");
        }

        static void ConnectToTwitch()
        {
            ConWin.UpdateTwitchLog("Twitch Client: Connecting...");
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
                Debug.Environment("Twitch Client -> Connected");
                ChatWriterTimer = Settings.GetChatWriterTime();
            }
            else
            {
                Debug.Environment("Twitch Client -> Failed to Connect");
                return;
            }
            ConWin.UpdateTwitchLog(Response);
            string Response2 = SReader.ReadLine();
            ConWin.UpdateTwitchLog(Response2);
        }

        #endregion

        #region Write/Read Chat

        public static void WriteToChat(string Msg)
        {
            SWriter.WriteLine("PRIVMSG #" + DisplayName.ToLower() + " :" + Msg);
            SWriter.Flush();
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
                ConWin.UpdateTwitchLog(Msg);
                if(Msg.Contains("PRIVMSG"))
                {
                    var splitPoint = Msg.IndexOf("!", 1);
                    var ChatName = Msg.Substring(0, splitPoint);
                    ChatName = ChatName.Substring(1);
                    splitPoint = Msg.IndexOf(":", 1);
                    Msg = Msg.Substring(splitPoint + 1);
                    if (Msg.Substring(0, 1) == "!" && Msg.Length <= 3)
                        MainClass.ChatOptions.CheckNewVote(Msg, ChatName);
                }
                else if (Msg.Contains("PING"))
                {
                    SWriter.WriteLine("PONG :tmi.twitch.tv");
                }
            }
            ChatWriterTimer -= 1;
            if(ChatWriterTimer <= 0)
            {
                WriteToChat("Type '!' and the number of the option you wish to vote for!");
                ChatWriterTimer = Settings.GetChatWriterTime();
            }
        }

        #endregion
    }
}
