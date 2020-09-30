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
                    try
                    {
                        Console.Clear();
                        Screen.PrintBoard(match.Board);

                        Console.WriteLine();
                        Console.WriteLine("Turn: " + match.Turns);
                        Console.WriteLine("Waiting player: " + match.ActualPlayer);
                        Console.WriteLine();

                        Console.Write("Origin: ");
                        Position origin = Screen.ReadChessPosition().ToPosition();
                        match.ValidateOriginPosition(origin);

                        bool[,] possibleMoves = match.Board.GetPiece(origin).PossibleMoves();

                        Console.Clear();
                        Screen.PrintBoard(match.Board, possibleMoves);

                        Console.WriteLine();
                        Console.Write("Destiny: ");
                        Position destiny = Screen.ReadChessPosition().ToPosition();

                        match.ExecutePlay(origin, destiny);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        Console.ReadKey();
                    }
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

