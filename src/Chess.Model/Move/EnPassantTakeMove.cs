using System.Drawing;
using Chess.Model.Models;

namespace Chess.Model.Move
{
    public sealed class EnPassantTakeMove : IMove
    {
        private readonly SimpleMove m_pawnMove;

        public EnPassantTakeMove() : this(new SimpleMove())
        {
        }

        private EnPassantTakeMove(SimpleMove pawnMove)
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

        /// <summary>
        /// The location where the Pawn to be taken is.
        /// </summary>
        public Point PawnLocation { get; set; }

        /// <inheritdoc />
        public Piece Apply(GameBoard board)
        {
            var taken = board[PawnLocation];
            board[PawnLocation] = Piece.CreateEmpty();

            m_pawnMove.Apply(board);

            return taken;
        }

        /// <inheritdoc />
        public IMove Clone() => new EnPassantTakeMove(m_pawnMove.SimpleMoveClone())
        {
            PawnLocation = PawnLocation
        };
    }
}