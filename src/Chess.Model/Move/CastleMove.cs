using System;
using System.Drawing;
using Chess.Model.Models;

namespace Chess.Model.Move
{
    public sealed class CastleMove : IMove
    {
        private readonly SimpleMove m_kingMove;

        public CastleMove() : this(new SimpleMove())
        {
        }

        private CastleMove(SimpleMove kingMove)
        {
            m_kingMove = kingMove;
        }
        
        /// <inheritdoc />
        public Piece Piece
        {
            get => m_kingMove.Piece;
            set => m_kingMove.Piece = value;
        }

        /// <inheritdoc />
        public Point From
        {
            get => m_kingMove.From;
            set => m_kingMove.From = value;
        }
        
        /// <inheritdoc />
        public Point To
        {
            get => m_kingMove.To;
            set => m_kingMove.To = value;
        }

        /// <summary>
        /// The move used to move the Rook while castling
        /// </summary>
        public SimpleMove RookMove { get; set; }

        /// <inheritdoc />
        public Piece Apply(GameBoard board)
        {
            m_kingMove.Apply(board);

            return RookMove.Apply(board);
        }

        public IMove Clone() => new CastleMove(m_kingMove.SimpleMoveClone())
        {
            RookMove = RookMove.SimpleMoveClone()
        };

        public override string ToString() =>
            Math.Abs(RookMove.From.X - RookMove.To.X) + Math.Abs(RookMove.From.Y - RookMove.To.Y) > 2 ? "O-O-O" : "O-O";
    }
}
