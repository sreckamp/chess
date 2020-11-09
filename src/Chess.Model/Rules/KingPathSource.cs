using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Chess.Model.Models;
using Chess.Model.Models.Board;

namespace Chess.Model.Rules
{
    public sealed class KingPathSource : IPathSource
    {
        // TODO: In Check?
        // TODO: Ensure that available moves get out of check
        // TODO: Side.KingLocation or KingSquare & GetChecks that returns the attack directions.  Can block.  It attack is "none" must move king or take attacking piece.

        /// <inheritdoc/>
        public IEnumerable<Path> GetPaths(Point start, Piece piece, IPieceEnumerationProvider squares)
            => piece.Type == PieceType.King
                ? Directions.All.Where(direction => squares.EnumerateStraightLine(start, direction).Any()).Select(direction => new Path
                    {
                        AllowMove = true,
                        AllowTake = true,
                        Direction = direction,
                        Start = start,
                        Piece = piece,
                        Squares = squares.EnumerateStraightLine(start, direction).Take(1)
                    })
                : Enumerable.Empty<Path>();
    }
}
