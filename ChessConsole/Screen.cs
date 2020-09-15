using System;
using System.Reflection;
using ChessConsole.BoardLayer;
using ChessConsole.ChessLayer;

namespace ChessConsole
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
            for (var j = 0; j < board.Columns; j++)
            {
                int letterInDecimal = 'a' + j;
                Console.Write((char)letterInDecimal);
                Console.Write(" ");
            }

            Console.WriteLine("\n");
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

        public static ChessPosition ReadChessPosition()
        {
            try
            {
                string s = Console.ReadLine();

                char column = s[0];

                int row = int.Parse(s[1] + "");

                return new ChessPosition(column, row);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error while executing [" + MethodBase.GetCurrentMethod().DeclaringType + " - " + MethodBase.GetCurrentMethod().Name + "]: " + e.Message);
                return null;
            }
        }
    }
}