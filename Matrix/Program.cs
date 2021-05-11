using System;
using System.Drawing;
using System.Diagnostics;
using System.Collections.Generic;
using Console = Colorful.Console;
using System.Threading;

namespace Matrix
{
    class Program
    {
        // Init
        static Random rand = new Random();
        static List<Point> drops = new List<Point>();

        // Settings
        static string AVALILABLE_CHARACTERS = "@#$%&QWERTYUIOPASDFGHJKLZXCVBNM123456987";
        static int MAX_DROPS_COUNT = 64;
        static int LOOP_DELAY = 0;
        static int SPAWN_DELAY = 10; //10ms
        static int DROP_LENGHT = 16;
        static int WINDOW_WIDTH = 60;
        static int WINDOW_HEIGHT = 30;

        static void Main(string[] args)
        {
            SetSize(WINDOW_WIDTH, WINDOW_HEIGHT);
            Console.CursorVisible = false;
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            // Main Loop
            while(true)
            {
                // Creating new drops
                if(stopwatch.Elapsed.TotalMilliseconds > SPAWN_DELAY)
                {
                    if(drops.Count < MAX_DROPS_COUNT)
                    {
                        drops.Add(new Point(rand.Next(Console.BufferWidth), 0));
                    }
                    stopwatch.Restart();
                }

                // Drawing
                for(int i =0;i< drops.ToArray().Length;i++)
                {
                    drops[i] = new Point(drops[i].X, drops[i].Y + 1);
                    Draw(drops[i], DROP_LENGHT);

                    if (drops[i].Y > Console.BufferHeight + DROP_LENGHT)
                    {
                        drops.RemoveAt(i);
                    }
                }
                Thread.Sleep(LOOP_DELAY);
            }
        }

        // Draw single drop
        static void Draw(Point drop,int lenght)
        {
            for(int i =0;i < lenght;i++)
            {
                int _y = drop.Y - i;
                int scaleRGB = (255 / lenght);

                if ((_y > 0) && (drop.Y < Console.BufferHeight))
                {
                    Console.SetCursorPosition(drop.X, (drop.Y - i));
                    Console.Write(AVALILABLE_CHARACTERS[rand.Next(AVALILABLE_CHARACTERS.Length)], Color.FromArgb(0, 255 - scaleRGB * i, 0));
                }
            }

            if (drop.Y - lenght > 0)
            {
                try
                {
                    Console.SetCursorPosition(drop.X, drop.Y - lenght);
                    Console.Write(' ');
                }
                catch{ } 
            } 
        }

        // Set window size
        static void SetSize(int width,int height)
        {
            Console.SetWindowSize(width, height);
            Console.SetBufferSize(width, height);
        }
    }
}
