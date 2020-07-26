namespace Stream_AFK_Text_Game
{
    static class Settings
    {
        static int VoteTimer, ConWinRefreshTimer, PauseTime;

        #region Get/Set Functions

        public static void SetVoteTimer(int NewVoteTimer)
        {
            VoteTimer = NewVoteTimer;
        }

        public static int GetVoteTimer()
        {
            return VoteTimer;
        }

        public static void SetConWinRefreshTimer(int NewConWinRefreshTimer)
        {
            ConWinRefreshTimer = NewConWinRefreshTimer;
        }

        public static int GetConWinRefreshTimer()
        {
            return ConWinRefreshTimer;
        }

        public static void SetPauseTime(int NewPauseTime)
        {
            PauseTime = NewPauseTime;
        }

        public static int GetPauseTime()
        {
            return PauseTime;
        }

        #endregion

        public static void LoadSettings()
        {
            VoteTimer = 30;
            ConWinRefreshTimer = 1000;
            PauseTime = 5000;
        }
    }
}
