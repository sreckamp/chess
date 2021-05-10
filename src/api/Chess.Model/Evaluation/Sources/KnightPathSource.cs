using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Chess.Model.Evaluation.Models;
using Chess.Model.Models;

namespace Chess.Model.Evaluation.Sources
{
    public sealed class KnightPathSource : IPathSource
    {
        /// <inheritdoc/>
        public IEnumerable<Path> GetPaths(Point start, Piece piece, IPieceEnumerationProvider squares)
            => piece.Type == PieceType.Knight
                ? squares.EnumerateKnight(start).Select(sq => new Path
                {
                    AllowMove = true,
                    AllowTake = true,
                    Start = start,
                    Piece = piece,
                    Direction = Direction.None,
                    Squares = new[] { sq }
                })
                : Enumerable.Empty<Path>();
    }
}
