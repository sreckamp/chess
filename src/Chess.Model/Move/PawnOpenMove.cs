using System;
using System.Drawing;
using Chess.Model.Models;
using Chess.Model.Models.Board;

namespace Chess.Model.Move
{
    /// <summary>
    /// Special move that a pawn can make when it has not been moved.  This moves two squares and marks enPassant
    /// </summary>
    public readonly struct PawnOpenMove : IMove
    {
        private readonly SimpleMove m_pawnMove;

        public PawnOpenMove(SimpleMove pawnMove)
        {
            m_pawnMove = pawnMove;
            EnPassant = new Point((pawnMove.From.X + pawnMove.To.X) / 2, (pawnMove.From.Y + pawnMove.To.Y) / 2);
        }
        
        /// <inheritdoc />
        public Point From => m_pawnMove.From;
        
        /// <inheritdoc />
        public Point To => m_pawnMove.To;

        /// <summary>
        /// The square eligible for an en passant capture
        /// </summary>
        public Point EnPassant { get; }

        /// <inheritdoc />
        public Piece Apply(IBoard board)
        {
            // TODO: Move this to the reducer
            // EnPassant.Mark(new SimpleMarker(MarkerType.EnPassant, To, Direction.None));

            return m_pawnMove.Apply(board);
        }
    }
}