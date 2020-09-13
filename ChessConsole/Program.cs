using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using chess_system_console;
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
                var board = new Board(8, 8);

                board.PlacePiece(new Rook(board, Color.Black), new Position(0, 0) );
                board.PlacePiece(new Rook(board, Color.Black), new Position(3,3) );
                board.PlacePiece(new King(board, Color.Black), new Position(0, 2) );

                board.PlacePiece(new Rook(board, Color.White), new Position(7, 0 ));
                board.PlacePiece(new Rook(board, Color.White), new Position(6, 7) );


                Screen.PrintScreen(board);

                Console.ReadKey();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            Console.ReadLine();
        }
    }
}

