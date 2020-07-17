using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
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

        public static void LaunchConnection()
        {
            GetTwitchDetails();
            ConnectToTwitch();
        }

        static void GetTwitchDetails()
        {
            Console.WriteLine("Twitch: Getting Details...");
            DisplayName = File.ReadAllText("Channel Details\\Display Name.txt");
            UserName = File.ReadAllText("Channel Details\\User Name.txt");
            AuthKey = File.ReadAllText("Channel Details\\Auth Key.txt");
        }

        static void ConnectToTwitch()
        {
            Console.WriteLine("Twitch: Connecting...");
            TwitchClient = new TcpClient("irc.chat.twitch.tv", 6667);
            SReader = new StreamReader(TwitchClient.GetStream());
            SWriter = new StreamWriter(TwitchClient.GetStream());
            SWriter.WriteLine("PASS " + AuthKey);
            SWriter.WriteLine("NICK " + "N\\A");
            SWriter.WriteLine("USER " + UserName + " 8 * :" + UserName);
            SWriter.WriteLine("JOIN #" + DisplayName);
            SWriter.Flush();
            string Response = SReader.ReadLine();
            Console.WriteLine(Response);
            if (Response.Contains("Welcome"))
            {
                Console.WriteLine("Twitch: Connected");
            }
            else
            {
                Console.WriteLine("Twitch: Failed to Connect");
            }
            string a = "PRIVMSG #" + DisplayName + " :" + "Test";
            SWriter.WriteLine(a);
            SWriter.Flush();
            Console.ReadLine();
        }
    }
}
