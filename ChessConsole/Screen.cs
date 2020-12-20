using System;
using System.Collections.Generic;
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

        internal static void PrintMatch(ChessMatch match)
        {
            PrintBoard(match.Board);
            Console.WriteLine();
            PrintCapturedPieces(match);

            Console.WriteLine("Turn: " + match.Turns);
            Console.WriteLine($"Waiting player: {match.ActualPlayer}");
            if(match.IsCheck)
            {
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("CHECK !!!");
                System.Console.ResetColor();
            }
            Console.WriteLine();
        }

        private static void PrintCapturedPieces(ChessMatch match)
        {
            Console.WriteLine("Captured pieces:");

            Console.Write("White: ");
            PrintSetPieces(match.GetCapturedPieces(Color.White));

            Console.Write("Black: ");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            PrintSetPieces(match.GetCapturedPieces(Color.Black));
            System.Console.ResetColor();
        }

        private static void PrintSetPieces(HashSet<Piece> pieces)
        {
            Console.Write("{ ");
            foreach (var piece in pieces)
            {
                Console.Write($"{piece} ");
            }
            Console.WriteLine("}");
        }

        public static void PrintBoard(Board board, bool[,] possibleMoves)
        {
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
                    PrintPiece(board.GetPiece(i, j));
                    System.Console.ResetColor();
                }

                Console.WriteLine();
            }
            // print the footer of the board
            Console.WriteLine("  a b c d e f g h");
            System.Console.ResetColor();
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
            Console.ForegroundColor = ConsoleColor.DarkYellow;
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