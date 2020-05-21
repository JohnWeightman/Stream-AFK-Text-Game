using System;
using TwitchLib.Client;
using TwitchLib.Client.Models;
using TwitchLib.Client.Events;
using TwitchLib.Client.Extensions;
using TwitchLib.Communication.Models;
using TwitchLib.Communication.Clients;

namespace Stream_AFK_Text_Game
{
    class TwitchChat
    {
        TwitchClient Client;
        string Token;
        string Channel;
        string UserName;

        public TwitchChat()
        {
            Token = IO.LoadAccessToken();
            Channel = IO.LoadChannel();
            UserName = IO.LoadUserName();
            ConnectionCredentials Credentials = new ConnectionCredentials(UserName, Token);
            var ClientOptions = new ClientOptions
            {
                MessagesAllowedInPeriod = 750,
                ThrottlingPeriod = TimeSpan.FromSeconds(30)
            };
            WebSocketClient CustomClient = new WebSocketClient(ClientOptions);
            Client = new TwitchClient(CustomClient);
            Client.Initialize(Credentials, Channel);
            Client.OnLog += Client_OnLog;
            Client.OnJoinedChannel += Client_OnJoinedChannel;
            Client.OnMessageReceived += Client_OnMessageReceived;
            Client.OnConnected += Client_OnConnected;
            Client.Connect();
        }

        #region GetSet Variables

        public string GetToken()
        {
            return Token;
        }

        public string GetChannel()
        {
            return Channel;
        }

        public string GetUserName()
        {
            return UserName;
        }

        #endregion

        private void Client_OnMessageReceived(object sender, OnMessageReceivedArgs e)
        {
            if (e.ChatMessage.Message.Contains("badword"))
                Client.TimeoutUser(e.ChatMessage.Channel, e.ChatMessage.Username, TimeSpan.FromMinutes(30), "Bad word! 30 minute timeout!");
            Console.WriteLine("Test Complete");
        }

        private void Client_OnLog(object sender, OnLogArgs e)
        {
            Console.WriteLine($"{e.DateTime.ToString()}: {e.BotUsername} - {e.Data}");
        }

        private void Client_OnConnected(object sender, OnConnectedArgs e)
        {
            Console.WriteLine($"Connected to {e.AutoJoinChannel}");
        }

        private void Client_OnJoinedChannel(object sender, OnJoinedChannelArgs e)
        {
            Console.WriteLine("Hey guys! I am a bot connected via TwitchLib!");
            Client.SendMessage(e.Channel, "Hey guys! I am a bot connected via TwitchLib!");
        }
    }
}
