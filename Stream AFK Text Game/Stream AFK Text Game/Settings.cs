using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stream_AFK_Text_Game
{
    static class Settings
    {
        static int VoteTimer;

        #region Get/Set Functions

        public static void SetVoteTimer(int NewVoteTimer)
        {
            VoteTimer = NewVoteTimer;
        }

        public static int GetVoteTimer()
        {
            return VoteTimer;
        }

        #endregion

        public static void LoadSettings()
        {
            VoteTimer = 30;
        }
    }
}
