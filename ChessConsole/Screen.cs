using System;
using System.Collections.Generic;
using System.Reflection;
using ChessConsole.BoardLayer;
using ChessConsole.ChessLayer;
using ChessConsole.Utils;

namespace ChessConsole
{
    internal static class Screen
    {
        public static void PrintBoard(Board board)
        {
            for (var i = 0; i < board.Rows; i++)
            {
                PrintBoardLeftHeader(i);

                for (var j = 0; j < board.Columns; j++)
                {
                    PrintPiece(board.GetPiece(i, j));
                }
                Console.WriteLine();
            }
            // print the bottom header of the board
            PrintBoardFooter();
        }

        private static void PrintBoardFooter()
        {
            ConsoleExt.WriteLineColored("  a b c d e f g h", ConsoleColor.Cyan);
        }

        private static void PrintBoardLeftHeader(int i)
        {
            ConsoleExt.WriteColored(8 - i + " ", ConsoleColor.Cyan);
        }

        internal static void PrintMatch(ChessMatch match)
        {
            PrintBoard(match.Board);
            Console.WriteLine();

            PrintCapturedPieces(match);
            Console.WriteLine();
            Console.WriteLine("Turn: " + match.Turns);

            if (!match.IsMatchCompleted)
            {
                Console.WriteLine($"Waiting player: {match.ActualPlayer}");
                if (match.IsCheck)
                {
                    ConsoleExt.WriteLineColored("CHECK!", ConsoleColor.Red);
                }
            }
            else 
            {
                ConsoleColor? color = match.ActualPlayer == Color.Black ? ConsoleColor.DarkYellow : Console.ForegroundColor;
                ConsoleExt.WriteLineColored("CHECK MATE!", color);
                ConsoleExt.WriteLineColored($"Player {match.ActualPlayer} Won! \n", color);
                Console.WriteLine("Press any key to exit...");
                Console.ReadLine();
            }

            Console.WriteLine();
        }

        private static void PrintCapturedPieces(ChessMatch match)
        {
            Console.WriteLine("Captured pieces:");

            Console.Write("White: ");
            var defaultColor = Console.ForegroundColor;
            PrintSetPieces(match.GetCapturedPieces(Color.White), defaultColor);

            Console.Write("Black: ");
            PrintSetPieces(match.GetCapturedPieces(Color.Black), ConsoleColor.DarkYellow);
            Console.ResetColor();
        }

        private static void PrintSetPieces(IEnumerable<Piece> pieces, ConsoleColor foregroundColor)
        {
            ConsoleExt.WriteColored("{ ", foregroundColor);
            foreach (var piece in pieces)
            {
                ConsoleExt.WriteColored($"{piece} ", foregroundColor);
            }
            ConsoleExt.WriteLineColored("}", foregroundColor);
        }

        public static void PrintBoard(Board board, bool[,] possibleMoves)
        {
            ConsoleColor highlightBackground = ConsoleColor.DarkGray;

            for (var i = 0; i < board.Rows; i++)
            {
                PrintBoardLeftHeader(i);

                for (var j = 0; j < board.Columns; j++)
                {
                    if (possibleMoves[i, j])
                    {
                        Console.BackgroundColor = highlightBackground;
                    }
                    PrintPiece(board.GetPiece(i, j));
                    Console.ResetColor();
                }

                Console.WriteLine();
            }
            PrintBoardFooter();
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

            ConsoleExt.WriteColored(piece + " ", ConsoleColor.DarkYellow);
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
            catch (Exception ex)
            {
                Console.WriteLine("Error while executing [" + MethodBase.GetCurrentMethod().DeclaringType + " - " + MethodBase.GetCurrentMethod().Name + "]: " + ex.Message);
                return null;
            }
        }
    }
}