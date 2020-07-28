using System;

namespace Stream_AFK_Text_Game
{
    static class Settings
    {
        static SerialSettings SettingsObj = new SerialSettings();
        //static int VoteTimer, ConWinRefreshTimer, PauseTime, ChatWriterTime;
        #region Get/Set Functions

        public static void SetVoteTimer(int NewVoteTimer)
        {
            SettingsObj.SetVoteTimer(NewVoteTimer);
        }

        public static int GetVoteTimer()
        {
            return SettingsObj.GetVoteTimer();
        }

        public static void SetConWinRefreshTimer(int NewConWinRefreshTimer)
        {
            SettingsObj.SetConWinRefreshTimer(NewConWinRefreshTimer);
        }

        public static int GetConWinRefreshTimer()
        {
            return SettingsObj.GetConWinRefreshTimer();
        }

        public static void SetPauseTime(int NewPauseTime)
        {
            SettingsObj.SetPauseTime(NewPauseTime);
        }

        public static int GetPauseTime()
        {
            return SettingsObj.GetPauseTime();
        }

        public static void SetChatWriterTime(int NewChatWriterTime)
        {
            SettingsObj.SetChatWriterTime(NewChatWriterTime);
        }

        public static int GetChatWriterTime()
        {
            return SettingsObj.GetChatWriterTime();
        }

        #endregion

        public static void LoadSettings()
        {
            SettingsObj.SetVoteTimer(30);
            SettingsObj.SetConWinRefreshTimer(1000);
            SettingsObj.SetPauseTime(5000);
            SettingsObj.SetChatWriterTime(900);
        }
    }

    [Serializable]
    class SerialSettings
    {
        static int VoteTimer, ConWinRefreshTimer, PauseTime, ChatWriterTime;

        #region Get/Set Functions

        public void SetVoteTimer(int NewVoteTimer)
        {
            VoteTimer = NewVoteTimer;
        }

        public int GetVoteTimer()
        {
            return VoteTimer;
        }

        public void SetConWinRefreshTimer(int NewConWinRefreshTimer)
        {
            ConWinRefreshTimer = NewConWinRefreshTimer;
        }

        public int GetConWinRefreshTimer()
        {
            return ConWinRefreshTimer;
        }

        public void SetPauseTime(int NewPauseTime)
        {
            PauseTime = NewPauseTime;
        }

        public int GetPauseTime()
        {
            return PauseTime;
        }

        public void SetChatWriterTime(int NewChatWriterTime)
        {
            ChatWriterTime = NewChatWriterTime;
        }

        public int GetChatWriterTime()
        {
            return ChatWriterTime;
        }

        #endregion
    }
}
