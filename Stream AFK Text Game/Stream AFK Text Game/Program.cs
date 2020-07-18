using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stream_AFK_Text_Game
{
    class Program
    {
        public static Player Player = new Player();

        static void Main(string[] args)
        {
            Console.WriteLine("Environment: Loading GameObjects...");
            GameObjects.LoadGameObjects();
            Console.WriteLine("Environment: Creating Character...");
            Player.CreateCharacter();
            Twitch.LaunchConnection();
            Player.CreateCharacter();
            Console.ReadLine();
        }
    }
}
