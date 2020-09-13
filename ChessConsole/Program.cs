using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessConsole.BoardLayer;
using ChessConsole.ChessLayer;

namespace chess_system_console
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                var chessPosition1 = new ChessPosition('A', 1);

                var chessPosition2 = new ChessPosition('C', 7);

                Console.WriteLine(chessPosition1.ToPosition());

                Console.WriteLine(chessPosition2.ToPosition());

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            Console.ReadLine();
        }
    }
}

