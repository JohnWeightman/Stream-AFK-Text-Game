using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Stream_AFK_Text_Game
{
    static class Settings
    {
        static SerialSettings SettingsObj = new SerialSettings();
        static IFormatter Formatter = new BinaryFormatter();
        static bool Pause;

        #region Get/Set Functions

        public static void SetVoteTimer(int NewVoteTimer)
        {
            SettingsObj.SetVoteTimer(NewVoteTimer);
            SaveSettingsToFile();
        }

        public static int GetVoteTimer()
        {
            return SettingsObj.GetVoteTimer();
        }

        public static void SetConWinRefreshTimer(int NewConWinRefreshTimer)
        {
            SettingsObj.SetConWinRefreshTimer(NewConWinRefreshTimer);
            SaveSettingsToFile();
        }

        public static int GetConWinRefreshTimer()
        {
            return SettingsObj.GetConWinRefreshTimer();
        }

        public static void SetPauseTime(int NewPauseTime)
        {
            SettingsObj.SetPauseTime(NewPauseTime);
            SaveSettingsToFile();
        }

        public static int GetPauseTime()
        {
            return SettingsObj.GetPauseTime();
        }

        public static void SetChatWriterTime(int NewChatWriterTime)
        {
            SettingsObj.SetChatWriterTime(NewChatWriterTime);
            SaveSettingsToFile();
        }

        public static int GetChatWriterTime()
        {
            return SettingsObj.GetChatWriterTime();
        }

        public static void SetPause(bool NewPause)
        {
            Pause = NewPause;
        }

        public static bool GetPause()
        {
            return Pause;
        }

        #endregion

        public static void LoadSettings()
        {
            SettingsObj.SetVoteTimer(30);
            SettingsObj.SetConWinRefreshTimer(1000);
            SettingsObj.SetPauseTime(5000);
            SettingsObj.SetChatWriterTime(900);
        }

        public static void LoadSettingsFromFile()
        {
            Stream Stream = new FileStream("Streamer Settings.dat", FileMode.Open, FileAccess.Read);
            SettingsObj = (SerialSettings)Formatter.Deserialize(Stream);
            Stream.Close();
        }

        public static void LoadDefaultSettings()
        {
            Stream Stream = new FileStream("Default Settings.dat", FileMode.Open, FileAccess.Read);
            SettingsObj = (SerialSettings)Formatter.Deserialize(Stream);
            SaveSettingsToFile();
            Stream.Close();
        }

        static void SaveSettingsToFile()
        {
            Stream Stream = new FileStream("Streamer Settings.dat", FileMode.Create, FileAccess.Write);
            Formatter.Serialize(Stream, SettingsObj);
            Stream.Close();
        }
    }

    [Serializable]
    class SerialSettings
    {
        int VoteTimer, ConWinRefreshTimer, PauseTime, ChatWriterTime;

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
