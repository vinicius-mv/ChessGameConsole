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
            var board = new Board(8, 8);

            board.PlacePiece(new Rook(board, Color.Black), new Position(0, 0));

            board.PlacePiece(new Rook(board, Color.Black), new Position(1, 3));

            board.PlacePiece(new King(board, Color.Black), new Position(3, 4)); ;

            Screen.PrintScreen(board);

            Console.ReadLine();

        }
    }
}

