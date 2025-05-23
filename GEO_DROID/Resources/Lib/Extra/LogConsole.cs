using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEO_DROID.Resources.Lib.Extra
{
    public class LogConsole
    {
        public static bool Enabled { get; set; }

        public static void print(string message)
        {
            if (!Enabled) return;

            print(ConsoleColor.Black, ConsoleColor.White, message);
        }

        public static void print(ConsoleColor color, string message)
        {
            if (!Enabled) return;

            print(ConsoleColor.Black, color, message);
        }

        public static void print(ConsoleColor background, ConsoleColor foreground, string message)
        {
            if (!Enabled) return;

            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.Write(DateTime.Now.ToString("HH:mm:ss.f"));

            Console.BackgroundColor = ConsoleColor.Black;
            Console.Write(" ");

            Console.BackgroundColor = background;
            Console.ForegroundColor = foreground;
            Console.WriteLine(message);
        }

    }
}
