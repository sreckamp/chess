using System;
using System.Drawing;
using Chess.Model.Models;

namespace Chess.Model.Move
{
    public class SimpleMove : IMove
    {
        /// <summary>
        /// The piece involved in the move.
        /// </summary>
        public Piece Piece { get; set; }

        /// <summary>
        /// The starting point of the move.
        /// </summary>
        public Point From { get; set; }
        
        /// <summary>
        /// The destination of the move.
        /// </summary>
        public Point To { get; set; }

        /// <summary>
        /// Apply this move to the game board
        /// </summary>
        /// <param name="board">The board to update with this move</param>
        /// <returns>Any captured piece (An empty piece if none is captured)</returns>
        public virtual Piece Apply(GameBoard board)
        {
            var taken = board[To.X, To.Y];

            board[To.X, To.Y] = board[From.X, From.Y];
            board[To.X, To.Y].Moved();
            board[From.X, From.Y] = Piece.CreateEmpty();

            return taken;
        }

        /// <summary>
        /// Standard Disambiguous Notation
        /// See https://en.wikipedia.org/wiki/Algebraic_notation_(chess)
        /// </summary>
        /// <returns></returns>
        public override string ToString() => Piece.Type == PieceType.Pawn 
            ? $"{ToString(To)}" 
            : $"{ToString(Piece.Type)}{ToString(From)}{ToString(To)}";

        public virtual IMove Clone() => SimpleMoveClone();

        internal SimpleMove SimpleMoveClone() => new SimpleMove
        {
            Piece = new Piece(Piece.Type, Piece.Color, Piece.Edge),
            From = From,
            To = To
        };

        private string ToString(PieceType type)
        {
            switch (type)
            {
                case PieceType.Empty:
                    return string.Empty;
                case PieceType.Pawn:
                    return string.Empty;
                case PieceType.Knight:
                    return "N";
                case PieceType.Bishop:
                    return "B";
                case PieceType.Rook:
                    return "R";
                case PieceType.Queen:
                    return "Q";
                case PieceType.King:
                    return "K";
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

        private string ToString(Point pt) => $"{(char)('a' + pt.X)}{pt.Y + 1}";
    }
}