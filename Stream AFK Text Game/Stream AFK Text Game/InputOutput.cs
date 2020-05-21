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
        public static string LoadAccessToken()
        {
            return File.ReadAllText("Channel Details\\TwitchAccessToken.txt");
        }

        public static string LoadChannel()
        {
            return File.ReadAllText("Channel Details\\TwitchChannel.txt");
        }

        public static string LoadUserName()
        {
            return File.ReadAllText("Channel Details\\TwitchUserName.txt");
        }
    }
}
