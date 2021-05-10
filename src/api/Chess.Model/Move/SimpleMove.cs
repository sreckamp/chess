using System.Drawing;
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
        // public override string ToString() => Piece.Type == PieceType.Pawn 
        //     ? $"{ToString(To.Location)}" 
        //     : $"{ToString(Piece.Type)}{ToString(From.Location)}{ToString(To.Location)}";

        // private string ToString(PieceType type)
        // {
        //     switch (type)
        //     {
        //         case PieceType.Empty:
        //             return string.Empty;
        //         case PieceType.Pawn:
        //             return string.Empty;
        //         case PieceType.Knight:
        //             return "N";
        //         case PieceType.Bishop:
        //             return "B";
        //         case PieceType.Rook:
        //             return "R";
        //         case PieceType.Queen:
        //             return "Q";
        //         case PieceType.King:
        //             return "K";
        //         default:
        //             throw new ArgumentOutOfRangeException(nameof(type), type, null);
        //     }
        // }
        //
        // private string ToString(Point pt) => $"{(char)('a' + pt.X)}{pt.Y + 1}";
    }
}