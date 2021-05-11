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
        static string availableCharacters = "@#$%&QWERTYUIOPASDFGHJKLZXCVBNM123456987";
        static Random rand = new Random();
        static List<Drop> drops = new List<Drop>();
        static void Main(string[] args)
        {
            SetSize(60, 30);
            Console.CursorVisible = false;
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            while(true)
            {
                Console.Title = drops.Count.ToString();
                if(stopwatch.Elapsed.TotalMilliseconds > 10)
                {
                    if(drops.Count < 36)
                    {
                        drops.Add(new Drop(Color.Green) { x = rand.Next(Console.BufferWidth) });
                        
                    }
                    stopwatch.Restart();
                }

                for(int i =0;i< drops.ToArray().Length;i++)
                {
                    drops[i].y += 1;
                    Draw(drops[i], 16);

                    if (drops[i].y > Console.BufferHeight + 16)
                    {
                        drops.RemoveAt(i);
                    }
                }
            }
        }

        static void Draw(Drop drop,int lenght)
        {
            for(int i =0;i < lenght;i++)
            {
                int _y = drop.y - i;
                int scaleRGB = (255 / lenght);

                if ((_y > 0) && (drop.y < Console.BufferHeight))
                {
                    Console.SetCursorPosition(drop.x, (drop.y - i));
                    Console.Write(availableCharacters[rand.Next(availableCharacters.Length)], Color.FromArgb(0, 255 - scaleRGB * i, 0));
                }
            }

            int __y = drop.y - lenght;
            if ((__y > 0) )
            {
                try
                {
                    Console.SetCursorPosition(drop.x, drop.y - lenght);
                    Console.Write(' ');
                }
                catch{ } 
            } 
        }

        static void SetSize(int width,int height)
        {
            Console.SetWindowSize(width, height);
            Console.SetBufferSize(width, height);
        }
    }
}
