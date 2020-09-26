using System.Collections.Generic;
using Chess.Model.Models;
using Chess.Model.Move;

namespace Chess.Model.Rules
{
    /// <summary>
    /// Description of a sequence of moves that is available.
    /// </summary>
    public class Path
    {
        /// <summary>
        /// If true this path is allowed to occupy empty squares
        /// </summary>
        public bool AllowMove { get; set; }

        /// <summary>
        /// If true this path is allowed to take opponent's pieces
        /// </summary>
        public bool AllowTake { get; set; }

        /// <summary>
        /// The direction the path travels
        /// </summary>
        public Direction Direction { get; set; }

        /// <summary>
        /// The sequence of moves that are possible in the given direction
        /// </summary>
        public IEnumerable<IMove> Moves { get; set; }
    }
}