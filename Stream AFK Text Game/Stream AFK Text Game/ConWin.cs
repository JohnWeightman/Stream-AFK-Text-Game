﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Stream_AFK_Text_Game
{
    static class ConWin
    {
        static List<string> TwitchLog = new List<string>();
        static List<string> DebugLog = new List<string>();
        static Timer Timer;
        static bool UDTwitchLog, UDDebugLog;

        #region Thread Functions

        public static void ConWinThreadStart()
        {
            DisplayVotingStatsLog(false);
            SetTimer();
        }

        static void SetTimer()
        {
            Timer = new Timer(100);
            Timer.Elapsed += UpdateDisplay;
            Timer.AutoReset = true;
            Timer.Enabled = true;
        }

        static void UpdateDisplay(Object source, ElapsedEventArgs e)
        {
            if(UDDebugLog)
                DisplayDebugLog();
            if(UDTwitchLog)
                DisplayTwitchLog();
            if(MainClass.ChatOptions.GetVote())
                DisplayVotingStatsLog(true);
        }

        #endregion

        #region Draw/Update Log Boxes

        public static void DrawGUI()
        {
            Console.SetWindowSize(171, 50);
            Console.SetBufferSize(172, 51);
            Draw.RectangleFromTop(170, 48, 0, 0, ConsoleColor.Blue);
            Draw.RectangleFromTop(170, 15, 0, 0, ConsoleColor.Blue);
            Draw.RectangleFromTop(50, 32, 0, 16, ConsoleColor.Blue);
            Draw.RectangleFromTop(50, 32, 120, 16, ConsoleColor.Blue);
            Draw.RectangleFromTop(70, 16, 50, 16, ConsoleColor.Blue);
        }

        static void DisplayTwitchLog()
        {
            UDTwitchLog = false;
            int y = 1;
            for(int i = 0; i < TwitchLog.Count; i++)
            {
                Console.SetCursorPosition(1, y);
                Console.Write(TwitchLog[i]);
                if(i - 1 >= 0)
                    if(TwitchLog[i - 1].Length > TwitchLog[i].Length)
                    {
                        int Count = TwitchLog[i - 1].Length - TwitchLog[i].Length;
                        for(int l = 0; l < Count; l++)
                        {
                            Console.SetCursorPosition(1 + TwitchLog[i].Length + l, y);
                            Console.Write(" ");
                        }
                    }
                y += 1;
            }
        }

        static void DisplayDebugLog()
        {
            UDDebugLog = false;
            int y = 17;
            for(int i = 0; i < DebugLog.Count; i++)
            {
                Console.SetCursorPosition(1, y);
                Console.Write(DebugLog[i]);
                if(i - 1 >= 0)
                    if(DebugLog[i - 1].Length > DebugLog[i].Length)
                    {
                        int Count = DebugLog[i - 1].Length - DebugLog[i].Length;
                        for(int l = 0; l < Count; l++)
                        {
                            Console.SetCursorPosition(1 + DebugLog[i].Length + l, y);
                            Console.Write(" ");
                        }
                    }
                y += 1;
            }
        }

        static void DisplayVotingStatsLog(bool Aval)
        {
            Console.SetCursorPosition(51, 17);
            if (!Aval)
            {
                Console.Write("Voting: ");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("CLOSED");
                Console.ForegroundColor = ConsoleColor.White;
                return;
            }
            Console.Write("Voting: ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("OPEN  ");
            Console.ForegroundColor = ConsoleColor.White;
            List<float> Votes = Debug.Stats.Voting.VotePercentage();
            Console.SetCursorPosition(51, 19);
            Console.Write("Live Votes:");
            int y = 21;
            for(int x = 1; x < Votes.Count + 1; x++)
            {
                Console.SetCursorPosition(51, y);
                Console.Write(x + ". " + Votes[x - 1] + "%     ");
                y += 1;
            }
        }

        static void DisplayGameStatsLog()
        {

        }

        static void DisplayStreamerInputLog()
        {

        }

        #endregion

        #region Update Logs

        public static void UpdateTwitchLog(string Log)
        {
            TwitchLog.Add(Log);
            if(TwitchLog.Count > 15)
                TwitchLog.RemoveAt(0);
            if (!UDTwitchLog)
                UDTwitchLog = true;
        }

        public static void UpdateDebugLog(string Log)
        {
            DebugLog.Add(Log);
            if (DebugLog.Count > 32)
                DebugLog.RemoveAt(0);
            if (!UDDebugLog)
                UDDebugLog = true;
        }

        #endregion
    }

    public static class Draw///Externally Sourced Class
    {
        /// <summary>
        /// Draws a rectangle in the console using several WriteLine() calls.
        /// </summary>
        /// <param name="width">The width of the rectangle.</param>
        /// <param name="height">The right of the rectangle.</param>
        /// <param name="xLocation">The left side position.</param>
        /// <param name="yLocation">The top position.</param>
        /// <param name="keepOriginalCursorLocation">If true, 
        /// the cursor will return back to the starting location.</param>
        /// <param name="color">The color to use. null=uses current color Default: null</param>
        /// <param name="useDoubleLines">Enables double line boarders. Default: false</param>
        public static void RectangleFromCursor(int width,
            int height,
            int xLocation = 0,
            int yLocation = 0,
            bool keepOriginalCursorLocation = false,
            ConsoleColor? color = null,
            bool useDoubleLines = false)
        {
            {
                // Save original cursor location
                int savedCursorTop = Console.CursorTop;
                int savedCursorLeft = Console.CursorLeft;

                // if the size is smaller then 1 then don't do anything
                if (width < 1 || height < 1)
                {
                    return;
                }

                // Save and then set cursor color
                ConsoleColor savedColor = Console.ForegroundColor;
                if (color.HasValue)
                {
                    Console.ForegroundColor = color.Value;
                }

                char tl, tt, tr, mm, bl, br;

                if (useDoubleLines)
                {
                    tl = '+'; tt = '-'; tr = '+'; mm = '¦'; bl = '+'; br = '+';
                }
                else
                {
                    tl = '+'; tt = '-'; tr = '+'; mm = '¦'; bl = '+'; br = '+';
                }

                for (int i = 0; i < yLocation; i++)
                {
                    Console.WriteLine();
                }

                Console.WriteLine(
                    string.Empty.PadLeft(xLocation, ' ')
                    + tl
                    + string.Empty.PadLeft(width - 1, tt)
                    + tr);

                for (int i = 0; i < height; i++)
                {
                    Console.WriteLine(
                        string.Empty.PadLeft(xLocation, ' ')
                        + mm
                        + string.Empty.PadLeft(width - 1, ' ')
                        + mm);
                }

                Console.WriteLine(
                    string.Empty.PadLeft(xLocation, ' ')
                    + bl
                    + string.Empty.PadLeft(width - 1, tt)
                    + br);


                if (color.HasValue)
                {
                    Console.ForegroundColor = savedColor;
                }

                if (keepOriginalCursorLocation)
                {
                    Console.SetCursorPosition(savedCursorLeft, savedCursorTop);
                }
            }
        }

        /// <summary>
        /// Draws a rectangle in a console window using the top line of the buffer as the offset.
        /// </summary>
        /// <param name="xLocation">The left side position.</param>
        /// <param name="yLocation">The top position.</param>
        /// <param name="width">The width of the rectangle.</param>
        /// <param name="height">The right of the rectangle.</param>
        /// <param name="color">The color to use. null=uses current color Default: null</param>
        public static void RectangleFromTop(
            int width,
            int height,
            int xLocation = 0,
            int yLocation = 0,
            ConsoleColor? color = null,
            bool useDoubleLines = false)
        {
            Rectangle(width, height, xLocation, yLocation, DrawKind.FromTop, color, useDoubleLines);
        }

        /// <summary>
        /// Specifies if the draw location should be based on the current cursor location or the
        /// top of the window.
        /// </summary>
        public enum DrawKind
        {
            BelowCursor,
            BelowCursorButKeepCursorLocation,
            FromTop,
        }

        /// <summary>
        /// Draws a rectangle in the console window.
        /// </summary>
        /// <param name="width">The width of the rectangle.</param>
        /// <param name="height">The right of the rectangle.</param>
        /// <param name="xLocation">The left side position.</param>
        /// <param name="yLocation">The top position.</param>
        /// <param name="drawKind">Where to draw the rectangle and 
        /// where to leave the cursor when finished.</param>
        /// <param name="color">The color to use. null=uses current color Default: null</param>
        /// <param name="useDoubleLines">Enables double line boarders. Default: false</param>
        public static void Rectangle(
            int width,
            int height,
            int xLocation = 0,
            int yLocation = 0,
            DrawKind drawKind = DrawKind.FromTop,
            ConsoleColor? color = null,
            bool useDoubleLines = false)
        {
            // if the size is smaller then 1 than don't do anything
            if (width < 1 || height < 1)
            {
                return;
            }

            // Save original cursor location
            int savedCursorTop = Console.CursorTop;
            int savedCursorLeft = Console.CursorLeft;

            if (drawKind == DrawKind.BelowCursor || drawKind == DrawKind.BelowCursorButKeepCursorLocation)
            {
                yLocation += Console.CursorTop;
            }

            // Save and then set cursor color
            ConsoleColor savedColor = Console.ForegroundColor;
            if (color.HasValue)
            {
                Console.ForegroundColor = color.Value;
            }

            char tl, tt, tr, mm, bl, br;

            if (useDoubleLines)
            {
                tl = '+'; tt = '-'; tr = '+'; mm = '¦'; bl = '+'; br = '+';
            }
            else
            {
                tl = '+'; tt = '-'; tr = '+'; mm = '¦'; bl = '+'; br = '+';
            }

            SafeDraw(xLocation, yLocation, tl);
            for (int x = xLocation + 1; x < xLocation + width; x++)
            {
                SafeDraw(x, yLocation, tt);
            }
            SafeDraw(xLocation + width, yLocation, tr);

            for (int y = yLocation + height; y > yLocation; y--)
            {
                SafeDraw(xLocation, y, mm);
                SafeDraw(xLocation + width, y, mm);
            }

            SafeDraw(xLocation, yLocation + height + 1, bl);
            for (int x = xLocation + 1; x < xLocation + width; x++)
            {
                SafeDraw(x, yLocation + height + 1, tt);
            }
            SafeDraw(xLocation + width, yLocation + height + 1, br);

            // Restore cursor
            if (drawKind != DrawKind.BelowCursor)
            {
                Console.SetCursorPosition(savedCursorLeft, savedCursorTop);
            }

            if (color.HasValue)
            {
                Console.ForegroundColor = savedColor;
            }
        }

        private static void SafeDraw(int xLocation, int yLocation, char ch)
        {
            if (xLocation < Console.BufferWidth && yLocation < Console.BufferHeight)
            {
                Console.SetCursorPosition(xLocation, yLocation);
                Console.Write(ch);
            }
        }
    }
}
