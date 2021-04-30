using System;
using System.Drawing;
using Chess.Model.Models;
using Chess.Model.Models.Board;

namespace Chess.Model.Move
{
    public readonly struct CastleMove : IMove
    {
        private readonly SimpleMove m_kingMove;

        public CastleMove(SimpleMove kingMove, SimpleMove rookMove)
        {
            m_kingMove = kingMove;
            RookMove = rookMove;
        }
        
        /// <inheritdoc />
        public Point From => m_kingMove.From;
        
        /// <inheritdoc />
        public Point To => m_kingMove.To;

        /// <summary>
        /// The move used to move the Rook while castling
        /// </summary>
        public SimpleMove RookMove { get; }

        /// <inheritdoc />
        public Piece Apply(IBoard board)
        {
            m_kingMove.Apply(board);

            return RookMove.Apply(board);
        }

        public override string ToString() =>
            Math.Abs(RookMove.From.X - RookMove.To.X)
            + Math.Abs(RookMove.From.Y - RookMove.To.Y) > 2 ? "O-O-O" : "O-O";
    }
}
