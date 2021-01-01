using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using ChessConsole.BoardLayer;
using ChessConsole.ChessLayer;
using ChessConsole.Utils;

namespace ChessConsole
{
    public static class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                var match = new ChessMatch();

                while (!match.IsMatchCompleted)
                {
                    try
                    {
                        Console.Clear();
                        Screen.PrintMatch(match);

                        Console.Write("Origin: ");
                        Position origin = Screen.ReadChessPosition()?.ToPosition();
                        match.ValidateOriginPosition(origin);

                        bool[,] possibleMoves = match.Board.GetPiece(origin).PossibleMoves();

                        Console.Clear();
                        Screen.PrintBoard(match.Board, possibleMoves);

                        Console.WriteLine();
                        Console.Write("Destination: ");
                        Position destination = Screen.ReadChessPosition()?.ToPosition();
                        match.ValidateDestinationPosition(origin, destination);

                        match.ExecutePlay(origin, destination);
                    }
                    catch (BoardException ex)
                    {
                        Console.WriteLine(ex.Message);
                        Console.ReadKey();
                    }
                    Console.Clear();
                    Screen.PrintMatch(match);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}

