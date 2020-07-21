using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stream_AFK_Text_Game
{
    static class Debug
    {
        public static Stats Stats = new Stats();
        static int LogNumber;

        public static void Log(string LogText)
        {
            LogNumber += 1;
            ConWin.UpdateDebugLog("Debug.Log #" + LogNumber + ": " + LogText);
        }
    }

    class Stats
    {
        public Encounters Encounters = new Encounters();
    }

    class Encounters
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
}
