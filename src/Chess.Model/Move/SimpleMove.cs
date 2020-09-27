using System;
using System.Drawing;
using Chess.Model.Models;

namespace Chess.Model.Move
{
    public sealed class SimpleMove : IMove
    {
        /// <inheritdoc />
        public Piece Piece { get; set; }

        /// <inheritdoc />
        public Point From { get; set; }
        
        /// <inheritdoc />
        public Point To { get; set; }

        /// <inheritdoc />
        public Piece Apply(GameBoard board)
        {
            var taken = board[To];

            board[To] = board[From];
            board[To].Moved();
            board[From] = Piece.CreateEmpty();

            return taken;
        }

        /// <inheritdoc />
        public IMove Clone() => SimpleMoveClone();

        internal SimpleMove SimpleMoveClone() => new SimpleMove
        {
            Piece = new Piece(Piece.Type, Piece.Color, Piece.Edge),
            From = From,
            To = To
        };

        /// <summary>
        /// Standard Disambiguous Notation
        /// See https://en.wikipedia.org/wiki/Algebraic_notation_(chess)
        /// </summary>
        /// <returns></returns>
        public override string ToString() => Piece.Type == PieceType.Pawn 
            ? $"{ToString(To)}" 
            : $"{ToString(Piece.Type)}{ToString(From)}{ToString(To)}";

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