using System.Drawing;
using Chess.Model.Models;

namespace Chess.Model.Move
{
    public sealed class PawnPromotionMove : IMove
    {
        private readonly SimpleMove m_pawnMove;

        public PawnPromotionMove() : this(new SimpleMove())
        {
        }

        private PawnPromotionMove(SimpleMove pawnMove)
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
            var taken = m_pawnMove.Apply(board);
            var pawn = board[m_pawnMove.To];

            board[m_pawnMove.To] = new Piece(PieceType.Queen, pawn.Color, pawn.Edge);

            return taken;
        }

        /// <inheritdoc />
        public IMove Clone() => new PawnPromotionMove(m_pawnMove.SimpleMoveClone());
    }
}