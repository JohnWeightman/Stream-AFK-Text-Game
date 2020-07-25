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
            Debug.Environment("Loading Settings...");
            Settings.LoadSettings();
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
            Debug.Environment("Loading GameObjects...");
            GameObjects.LoadGameObjects();
            Debug.Environment("Creating Character...");
            Player.CreateCharacter();
            Debug.Environment("Resetting Files...");
            IO.ResetFiles(Player);
            Debug.Environment("Starting Twitch Client...");
            Twitch.LaunchConnection();
            System.Threading.Thread.Sleep(10000);
            while (true)
            {
                List<EnemyNPC> Temp = SelectEnemies();
                Encounter.StartEncounter(Temp, Player);
            }
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
            {
                Options[VoteNum - 1] += 1;
                Debug.Stats.Voting.NewVote(VoteNum - 1);
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
            Seconds -= 1;
            if(Seconds <= 0)
            {
                Timer.AutoReset = false;
                Vote = false;
            }
            IO.VoteTimer(Seconds);
        }
    }
}
