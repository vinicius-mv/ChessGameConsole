using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessConsole.Utils
{
    public static class ConsoleExt
    {
        public static void WriteLineColored(string message, ConsoleColor? foregroundColor)
        {
            if (foregroundColor == null) { foregroundColor = Console.ForegroundColor; }
            message = message + "\n";
            WriteColored(message, foregroundColor);
        }

        public static void WriteColored(string message, ConsoleColor? foregroundColor)
        {
            if (foregroundColor == null)
            {
                Console.Write(message);
                return;
            }
            var oldColor = Console.ForegroundColor;
            Console.ForegroundColor = (ConsoleColor)foregroundColor;
            Console.Write(message);
            Console.ForegroundColor = oldColor;
        }
    }
}
