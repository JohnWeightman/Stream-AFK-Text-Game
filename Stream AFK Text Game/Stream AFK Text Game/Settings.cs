namespace Stream_AFK_Text_Game
{
    static class Settings
    {
        static int VoteTimer, ConWinRefreshTimer, PauseTime, ChatWriterTime;

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

        public static void SetChatWriterTime(int NewChatWriterTime)
        {
            ChatWriterTime = NewChatWriterTime;
        }

        public static int GetChatWriterTime()
        {
            return ChatWriterTime;
        }

        #endregion

        public static void LoadSettings()
        {
            VoteTimer = 30;
            ConWinRefreshTimer = 1000;
            PauseTime = 5000;
            ChatWriterTime = 900;
        }
    }
}
