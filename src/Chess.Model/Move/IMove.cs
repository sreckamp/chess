using System.Drawing;
using Chess.Model.Models;

namespace Chess.Model.Move
{
    public interface IMove
    {
        /// <summary>
        /// The piece involved in the move.
        /// </summary>
        Piece Piece { get; }

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
        /// <param name="board">The board to update with this move</param>
        /// <returns>Any captured piece (An empty piece if none is captured)</returns>
        Piece Apply(GameBoard board);

        /// <summary>
        /// Clone the existing move
        /// </summary>
        /// <returns>A new instance that is the same type and action</returns>
        IMove Clone();
    }
}