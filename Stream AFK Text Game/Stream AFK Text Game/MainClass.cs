using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Stream_AFK_Text_Game
{
    class MainClass
    {
        static Player Player = new Player();
        public static ChatOptions ChatOptions = new ChatOptions();

        static void Main(string[] args)
        {
            ConWin.DrawGUI();
            ConWin.UpdateDebugLog("Environment: Loading GameObjects...");
            GameObjects.LoadGameObjects();
            ConWin.UpdateDebugLog("Environment: Creating Character...");
            Player.CreateCharacter();
            ConWin.UpdateDebugLog("Environment: Resetting Files...");
            IO.ResetFiles(Player);
            ConWin.UpdateDebugLog("Environment: Starting Twitch Client...");
            System.Threading.Thread GameThread = new System.Threading.Thread(new System.Threading.ThreadStart(GameThreadStart));
            System.Threading.Thread ConWinThread = new System.Threading.Thread(new System.Threading.ThreadStart(ConWin.ConWinThreadStart));
            GameThread.Start();
            ConWinThread.Start();
        }

        public static int ChatInput(int OptNum)
        {
            ChatOptions.SetNumberOfOptions(OptNum);
            ChatOptions.SetVote(true);
            ChatOptions.SetTimer();
            Twitch.WriteToChat("Type '!' and the number of the option you wish to vote for!\n\nOptions: 1-" + OptNum + ", EG '!1'");
            int ChatChoice = 0;
            while (ChatOptions.GetVote())
            {
                System.Threading.Thread.Sleep(1000);
                ChatChoice = ChatOptions.MostVoted();
            }
            return ChatChoice;
        }

        static void GameThreadStart()
        {
            Twitch.LaunchConnection();
            System.Threading.Thread.Sleep(10000);
            while (true)
            {
                List<EnemyNPC> Temp = new List<EnemyNPC>();
                Encounter.StartEncounter(Temp, Player);
            }
        }
    }

    class ChatOptions
    {
        Timer Timer;
        List<int> Options = new List<int>();
        int OptNum;
        int Seconds = 30;
        bool Vote;

        public void SetVote(bool NewVote)
        {
            Vote = NewVote;
        }

        public bool GetVote()
        {
            return Vote;
        }

        public void SetNumberOfOptions(int OptionNumber)
        {
            Options.Clear();
            OptNum = OptionNumber;
            for (int i = 0; i < OptNum; i++)
                Options.Add(0);
        }

        public int MostVoted()
        {
            int Max = Options.Max();
            int Pos;
            for (Pos = 0; Pos < Options.Count; Pos++)
                if (Options[Pos] == Max)
                    return Pos + 1;
            return -1;
        }

        public void CheckNewVote(string VoteString)
        {
            VoteString = VoteString.Substring(1, VoteString.Length - 1);
            int VoteNum;
            bool Result = int.TryParse(VoteString, out VoteNum);
            if (!Result)
                return;
            else if (Result && Vote && VoteNum <= OptNum && VoteNum >= 1)
                Options[VoteNum - 1] += 1;
        }

        public void SetTimer()
        {
            Timer = new Timer(1000);
            Timer.Elapsed += OnTimedEvent;
            Timer.AutoReset = true;
            Timer.Enabled = true;
        }

        void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            Seconds -= 1;
            if(Seconds <= 0)
            {
                Timer.AutoReset = false;
                Vote = false;
                Seconds = 30;
            }
            IO.VoteTimer(Seconds);
        }

    }
}
