using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;

namespace Stream_AFK_Text_Game
{
    class MainClass
    {
        public static Player Player = new Player();
        public static ChatOptions ChatOptions = new ChatOptions();

        static void Main(string[] args)
        {
            ConWin.DrawGUI();
            IO.GameUpdate("Game Starting...");
            IO.Options(null);
            Debug.Environment("Loading Settings...");
            Settings.LoadSettingsFromFile();           
            System.Threading.Thread GameThread = new System.Threading.Thread(new System.Threading.ThreadStart(GameThreadStart));
            System.Threading.Thread ConWinThread = new System.Threading.Thread(new System.Threading.ThreadStart(ConWin.ConWinThreadStart));
            GameThread.Start();
            ConWinThread.Start();
        }

        public static int ChatInput(int OptNum)
        {
            Debug.Stats.Voting.SetOptionNumber(OptNum);
            ChatOptions.SetNumberOfOptions(OptNum);
            ChatOptions.SetVote(true);
            ChatOptions.SetTimer();
            int ChatChoice = -1;
            while (ChatOptions.GetVote() || ChatChoice == -1)
            {
                System.Threading.Thread.Sleep(1000);
                ChatChoice = ChatOptions.MostVoted();
            }
            return ChatChoice;
        }

        static void GameThreadStart()
        {
            Debug.Environment("Starting Twitch Client...");
            Twitch.LaunchConnection();
            System.Threading.Thread.Sleep(5000);
            GameLoop();
        }

        static void GameLoop()
        {
            IO.GameUpdate("Game Starting...");
            Debug.Environment("Creating Character...");
            Player.CreateCharacter();
            Debug.Environment("Resetting Files...");
            IO.ResetFiles(Player);
            Debug.Environment("Starting World Generator...");
            Campaign Campaign = ProGen.GenerateNewAdventure();
            Twitch.WriteToChat("Type '!' and the number of the option you wish to vote for!");
            System.Threading.Thread.Sleep(Settings.GetPauseTime());
            while (!Player.GetDead())
            {
                //List<EnemyNPC> Temp = SelectEnemies();
                //Encounter.StartEncounter(Temp, Player);
            }
            IO.GameUpdate("YOU DIED");
            System.Threading.Thread.Sleep(Settings.GetPauseTime());
            GameLoop();
        }

        static List<EnemyNPC> SelectEnemies()
        {
            List<EnemyNPC> EnemyNPCs = new List<EnemyNPC>();
            if (Player.GetLevel() == 1)
            {
                int Ran = DiceRoller.RollDice(2) - 1;
                EnemyNPC Foe = new EnemyNPC();
                Foe = GameObjects.NPCs[Ran];
                EnemyNPCs.Add(Foe);
            }
            else
            {
                int Ran = DiceRoller.RollDice(6);
                switch (Ran)
                {
                    case 1:
                    case 2:
                        Ran = Player.GetLevel() - 1;
                        break;
                    case 3:
                    case 4:
                    case 5:
                        Ran = Player.GetLevel();
                        break;
                    case 6:
                        Ran = Player.GetLevel() + 1;
                        break;
                }
                List<EnemyNPC> Temp = new List<EnemyNPC>();
                foreach (EnemyNPC NPC in GameObjects.NPCs)
                    if (NPC.DifBonus == Ran)
                        Temp.Add(NPC);
                Ran = DiceRoller.RollDice(Temp.Count);
                EnemyNPC Foe = Temp[Ran];
                EnemyNPCs.Add(Foe);
            }
            return EnemyNPCs;
        }
    }

    class ChatOptions
    {
        Timer Timer;
        List<int> Options = new List<int>();
        List<string> Voters = new List<string>();
        int OptNum, Seconds;
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
            if(Options.Sum(x => Convert.ToInt32(x)) > 0)
            {
                int Max = Options.Max();
                int Pos;
                for (Pos = 0; Pos < Options.Count; Pos++)
                    if (Options[Pos] == Max)
                        return Pos + 1;
            }
            return -1;
        }

        public void CheckNewVote(string VoteString, string NewUserName)
        {
            VoteString = VoteString.Substring(1, VoteString.Length - 1);
            int VoteNum;
            bool Result = int.TryParse(VoteString, out VoteNum);
            if (!Result)
                return;
            else if (Result && Vote && VoteNum <= OptNum && VoteNum >= 1)
            {
                bool Voted = false;
                foreach (string UserName in Voters)
                    if (UserName == NewUserName)
                        Voted = true;
                if (!Voted)
                {
                    Voters.Add(NewUserName);
                    Options[VoteNum - 1] += 1;
                    Debug.Stats.Voting.NewVote(VoteNum - 1);
                }
            }
        }

        public void SetTimer()
        {
            Seconds = Settings.GetVoteTimer();
            Timer = new Timer(1000);
            Timer.Elapsed += OnTimedEvent;
            Timer.AutoReset = true;
            Timer.Enabled = true;
        }

        void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            if (!Settings.GetPause())
            {
                Seconds -= 1;
                if (Seconds <= 0)
                {
                    if (Debug.Stats.Voting.GetTotalVotes() > 0)
                    {
                        Timer.AutoReset = false;
                        Vote = false;
                        Voters.Clear();
                    }
                    else
                        Seconds = Settings.GetVoteTimer();
                }
                IO.VoteTimer(Seconds);
            }
        }
    }
}
