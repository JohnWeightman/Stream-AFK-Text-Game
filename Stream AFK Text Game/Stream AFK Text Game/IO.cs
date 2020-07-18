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
        public static void PlayerName(string Name)
        {
            File.WriteAllText("Stream Files\\1. Character Name.txt", Name);
        }

        public static string GetPlayerName()
        {
            return File.ReadAllText("Stream FIles\\1. Character Name.txt");
        }
    }
}
