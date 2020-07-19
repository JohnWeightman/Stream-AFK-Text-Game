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
        public static Player Player = new Player();
        public static ChatOptions ChatOptions = new ChatOptions();

        static void Main(string[] args)
        {
            Console.WriteLine("Environment: Loading GameObjects...");
            GameObjects.LoadGameObjects();
            Console.WriteLine("Environment: Creating Character...");
            Player.CreateCharacter();
            Twitch.LaunchConnection();
            List<EnemyNPC> Temp = new List<EnemyNPC>();
            Encounter.StartEncounter(Temp);
            Console.ReadLine();
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
    }

    class ChatOptions
    {
        Timer Timer;
        List<int> Options = new List<int>();
        int OptNum;
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
                    return Pos;
            return -1;
        }

        public void CheckNewVote(string VoteString)
        {
            OptNum = 3;
            VoteString = VoteString.Substring(1, VoteString.Length - 1);
            int Vote;
            bool Result = int.TryParse(VoteString, out Vote);
            if (!Result)
                return;
            else if (Result && Vote <= OptNum && Vote >= 1)
                Options[Vote - 1] += 1;
        }

        public void SetTimer()
        {
            Timer = new Timer(30000);
            Timer.Elapsed += OnTimedEvent;
            Timer.AutoReset = false;
            Timer.Enabled = true;
        }

        void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            Vote = false;
        }

    }
}
