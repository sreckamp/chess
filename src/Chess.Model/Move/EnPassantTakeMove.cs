using System.Drawing;
using Chess.Model.Models;
using Chess.Model.Models.Board;

namespace Chess.Model.Move
{
    public readonly struct EnPassantTakeMove : IMove
    {
        private readonly SimpleMove m_pawnMove;
        private readonly Point m_pawnLocation;

        public EnPassantTakeMove(Point from, Point to, Point pawnLocation)
        {
            m_pawnMove = new SimpleMove(from, to);
            m_pawnLocation = pawnLocation;
        }

        /// <inheritdoc />
        public Point From => m_pawnMove.From;
        
        /// <inheritdoc />
        public Point To => m_pawnMove.To;

        /// <summary>
        /// The location where the Pawn to be taken is.
        /// </summary>

        /// <inheritdoc />
        public Piece Apply(IBoard board)
        {
            var taken = board[m_pawnLocation];

            m_pawnMove.Apply(board);

            return taken;
        }
    }
}