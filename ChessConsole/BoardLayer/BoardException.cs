using System;

namespace ChessConsole.BoardLayer
{
    internal class BoardException : Exception
    {
        public BoardException(string msg) : base(msg)
        {
        }
    }
}
