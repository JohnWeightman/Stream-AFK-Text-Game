using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace Stream_AFK_Text_Game
{
    static class Debug
    {
        public static Stats Stats = new Stats();
        static int LogNumber;
        static string DebugPath;

        public static void CreateDebugFile()
        {
            int FileCount = Directory.GetFiles("Debug", "*", SearchOption.TopDirectoryOnly).Length + 1;
            string Output = "**************************************************\n\nDEBUG LOG #" + FileCount + "\n" + DateTime.Now.ToString() +
                "\n\n**************************************************\n\n";
            DebugPath = "Debug\\Debug Log #" + FileCount + ".txt";
            File.WriteAllText(DebugPath, Output);
        }

        static void UpdateDebugFile(string LogText)
        {
            using (StreamWriter SW = File.AppendText(DebugPath))
            {
                SW.WriteLine(LogText);
            }
        }

        public static void Log(string LogText)
        {
            LogNumber += 1;
            if (LogNumber == 1)
                CreateDebugFile();
            ConWin.UpdateDebugLog("Debug.Log #" + LogNumber + ": " + LogText);
            UpdateDebugFile("Debug.Log #" + LogNumber + ": " + LogText);
        }

        public static void Environment(string LogText)
        {
            ConWin.UpdateDebugLog("Environment: " + LogText);
        }

        public static void WG(string LogText)
        {
            ConWin.UpdateDebugLog("WG: " + LogText);
        }
    }

    class Stats
    {
        public EncounterStats Encounters = new EncounterStats();
        public VotingStats Voting = new VotingStats();
    }

    class EncounterStats
    {
        int FightNumber;

        #region Get/Set Functions

        public void SetFightNumber(int NewFightNumber)
        {
            FightNumber = NewFightNumber;
        }

        public int GetFightNumber()
        {
            return FightNumber;
        }

        #endregion
    }

    class VotingStats
    {
        int OptionNumber;
        List<int> OptionVotes = new List<int>();

        #region Get/Set Functions

        public void SetOptionNumber(int NewOptionNumber)
        {
            OptionNumber = NewOptionNumber;
            OptionVotes.Clear();
            for (int x = 0; x < OptionNumber; x++)
                OptionVotes.Add(0);
        }

        public int GetOptionNumber()
        {
            return OptionNumber;
        }

        public int GetTotalVotes()
        {
            return OptionVotes.Sum(x => Convert.ToInt32(x));
        }

        public void NewVote(int Vote)
        {
            OptionVotes[Vote] += 1;
        }

        #endregion

        #region Vote Stats functions

        public List<float> VotePercentage()
        {
            List<float> VotePercents = new List<float>();
            int Total = OptionVotes.Sum(x => Convert.ToInt32(x));
            foreach(int Count in OptionVotes)
            {
                float PerCent = (float)Math.Round((float)Count / (float)Total * (float)100, 1);
                VotePercents.Add(PerCent);
            }
            return VotePercents;
        }

        #endregion
    }
}
