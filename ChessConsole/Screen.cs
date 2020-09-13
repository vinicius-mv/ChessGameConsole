using System;
using System.Runtime.InteropServices;
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
                    // print the left header of the board
                    if (j == 0)
                    {
                        Console.Write(board.Rows - i + " ");
                    }

                    // print pieces
                    if (board.GetPiece(i, j) == null)
                    {
                        Console.Write("- ");
                    }
                    else
                    {
                        Screen.PrintPiece(board.GetPiece(i, j));
                        Console.Write(" ");
                    }
                }
                Console.WriteLine();
            }

            // print the botton header of the board
            Console.Write("  ");
            for (var k = 0; k < board.Columns; k++)
            {
                int letterInDecimal = 'a' + k;
                Console.Write((char)letterInDecimal);
                Console.Write(" ");
            }
        }

        public static void PrintPiece(Piece piece)
        {
            if (piece.Color == Color.White)
            {
                Console.Write(piece);
                return;
            }

            var originalColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(piece);
            Console.ForegroundColor = originalColor;
        }
    }
}