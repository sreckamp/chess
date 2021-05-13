using System.Drawing;
using Chess.Model.Extensions;
using Chess.Model.Models;

namespace Chess.Model.Move
{
    public readonly struct SimpleMove : IMove
    {
        public SimpleMove(Point from, Point to)
        {
            From = from;
            To = to;
        }
        
        /// <inheritdoc />
        public Point From { get; }
        
        /// <inheritdoc />
        public Point To { get; }

        /// <inheritdoc />
        public Piece Apply(IBoard board)
        {
            var taken = board[To];

            var moved = board[From];
            board[From] = Piece.Empty;

            moved.Moved();

            board[To] = moved;

            return taken;
        }

        /// <summary>
        /// Standard Disambiguous Notation
        /// See https://en.wikipedia.org/wiki/Algebraic_notation_(chess)
        /// </summary>
        /// <returns></returns>
        public override string ToString() => $"{To.ToChessPosition()}";
    }
}