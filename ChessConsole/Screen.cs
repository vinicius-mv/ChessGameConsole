using System;
using System.Reflection;
using ChessConsole.BoardLayer;
using ChessConsole.ChessLayer;

namespace ChessConsole
{
    internal static class Screen
    {
        public static void PrintBoard(Board board)
        {
            for (var i = 0; i < board.Rows; i++)
            {
                // print the left header of the board
                Console.Write(8 - i + " ");

                for (var j = 0; j < board.Columns; j++)
                {
                    PrintPiece(board.GetPiece(i, j));
                }

                Console.WriteLine();
            }
            // print the bottom header of the board
            Console.WriteLine("  a b c d e f g h");
        }

        public static void PrintBoard(Board board, bool[,] possibleMoves)
        {
            ConsoleColor originalBackground = Console.BackgroundColor;

            const ConsoleColor highlightBackground = ConsoleColor.DarkGray; 

            for (var i = 0; i < board.Rows; i++)
            {
                // print the left header of the board
                Console.Write(8 - i + " ");

                for (var j = 0; j < board.Columns; j++)
                {
                    if (possibleMoves[i, j])
                    {
                        Console.BackgroundColor = highlightBackground;
                    }
                    else
                    {
                        Console.BackgroundColor = originalBackground;
                    }
                    PrintPiece(board.GetPiece(i, j));
                }

                Console.WriteLine();
            }
            // print the footer of the board
            Console.WriteLine("  a b c d e f g h");
            Console.BackgroundColor = originalBackground;
        }

        public static void PrintPiece(Piece piece)
        {
            if (piece == null)
            {
                Console.Write("- ");
                return;
            }

            if (piece.Color == Color.White)
            {
                Console.Write(piece + " ");
                return;
            }

            // piece.Color == Black(Yellow)
            ConsoleColor originalColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(piece + " ");
            Console.ForegroundColor = originalColor;


        }

        public static ChessPosition ReadChessPosition()
        {
            try
            {
                string input = Console.ReadLine();

                char column = input[0];

                int row = int.Parse(input[1] + "");

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