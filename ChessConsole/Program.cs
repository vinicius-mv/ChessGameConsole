using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using ChessConsole.BoardLayer;
using ChessConsole.ChessLayer;

namespace ChessConsole
{
    public static class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                var match = new ChessMatch();

                while (!match.IsFinished)
                {
                    Console.Clear();
                    Screen.PrintBoard(match.Board);

                    Console.Write("Origin: ");
                    Position origin = Screen.ReadChessPosition().ToPosition();

                    bool[,] possibleMoves = match.Board.GetPiece(origin).PossibleMoves();

                    Console.Clear();
                    Screen.PrintBoard(match.Board, possibleMoves);

                    Console.Write("Destiny: ");
                    Position destiny = Screen.ReadChessPosition().ToPosition();

                    match.ExecuteMove(origin, destiny);
                }
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            Console.ReadLine();
        }
    }
}

