using System;
using ChessConsole.BoardLayer;

namespace chess_system_console
{
    internal static class Screen
    {
        public static void PrintScreen(Board board)
        {
            for (var i = 0; i < board.Rows; i++)
            {
                for (var j = 0; j < board.Columns; j++)
                {
                    if (board.GetPiece(i, j) == null)
                    {
                        Console.Write("- ");
                    }
                    else
                    {
                        Console.Write(board.GetPiece(i, j) + " ");
                    }
                }
                Console.WriteLine();
            }
        }
    }
}