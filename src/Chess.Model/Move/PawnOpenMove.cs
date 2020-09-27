using System;
using System.Drawing;
using Chess.Model.Extensions;
using Chess.Model.Models;
using Chess.Model.Models.Board;

namespace Chess.Model.Move
{
    /// <summary>
    /// Special move that a pawn can make when it has not been moved.  This moves two squares and marks enPassant
    /// </summary>
    public sealed class PawnOpenMove : IMove
    {
        private readonly SimpleMove m_pawnMove;

        public PawnOpenMove() : this(new SimpleMove())
        {
        }

        private PawnOpenMove(SimpleMove pawnMove)
        {
            m_pawnMove = pawnMove;
        }
        
        /// <inheritdoc />
        public Piece Piece
        {
            get => m_pawnMove.Piece;
            set => m_pawnMove.Piece = value;
        }

        /// <inheritdoc />
        public Point From
        {
            get => m_pawnMove.From;
            set => m_pawnMove.From = value;
        }
        
        /// <inheritdoc />
        public Point To
        {
            get => m_pawnMove.To;
            set => m_pawnMove.To = value;
        }

        /// <inheritdoc />
        public Piece Apply(GameBoard board)
        {
            board.GetSquare(From.CartesianOffset(To).Divide(2)).Mark(
                new SimpleMarker(MarkerType.EnPassant, board.GetSquare(To), Direction.None));

            return m_pawnMove.Apply(board);
        }

        public IMove Clone() => new PawnOpenMove(m_pawnMove.SimpleMoveClone());
    }
}