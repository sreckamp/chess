using System.Drawing;
using Chess.Model.Models;
using Chess.Model.Models.Board;

namespace Chess.Model.Move
{
    public readonly struct PawnPromotionMove : IMove
    {
        private readonly SimpleMove m_pawnMove;

        public PawnPromotionMove(SimpleMove pawnMove)
        {
            m_pawnMove = pawnMove;
        }

        /// <inheritdoc />
        public Point From => m_pawnMove.From;

        /// <inheritdoc />
        public Point To => m_pawnMove.To;

        /// <inheritdoc />
        public Piece Apply(IBoard board)
        {
            var taken = m_pawnMove.Apply(board);
            var pawn = board[m_pawnMove.To];

            board[m_pawnMove.To] = new Piece(PieceType.Queen, pawn.Color, pawn.Edge);

            return taken;
        }
    }
}