using System.Drawing;
using Chess.Model.Models;

namespace Chess.Model.Move
{
    public interface IMove
    {
        /// <summary>
        /// The starting point of the move.
        /// </summary>
        Point From { get; }

        /// <summary>
        /// The destination of the move.
        /// </summary>
        Point To { get; }

        /// <summary>
        /// Apply this move to the game board
        /// </summary>
        /// <returns>Any captured piece (An empty piece if none is captured)</returns>
        Piece Apply(IBoard board);
    }
}