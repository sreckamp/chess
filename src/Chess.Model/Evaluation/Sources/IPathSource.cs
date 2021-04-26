using System.Collections.Generic;
using System.Drawing;
using Chess.Model.Models;
using Chess.Model.Models.Board;

namespace Chess.Model.Rules
{
    public interface IPathSource
    {
        /// <summary>
        /// Returns the potential paths from the given square
        /// </summary>
        /// <param name="start">The starting location</param>
        /// <param name="piece">The piece at the starting location</param>
        /// <param name="squares">The source of squares</param>
        /// <returns></returns>
        IEnumerable<Path> GetPaths(Point start, Piece piece, IPieceEnumerationProvider squares);
    }
}