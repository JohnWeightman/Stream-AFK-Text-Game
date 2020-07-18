using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Stream_AFK_Text_Game
{
    static class IO
    {
        #region Player

        #region Stats

        public static void PlayerName(string Name)
        {
            File.WriteAllText("Stream Files\\Player\\1. Stats\\1. Name.txt", Name);
        }

        public static string GetPlayerName()
        {
            return File.ReadAllText("Stream FIles\\Player\\1. Stats\\1. Name.txt");
        }

        #endregion

        #region Equipment

        #endregion

        #endregion
    }
}
