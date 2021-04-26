using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Chess.Model.Models;
using Chess.Model.Models.Board;

namespace Chess.Model.Rules
{
    public sealed class KnightPathSource : IPathSource
    {
        /// <inheritdoc/>
        public IEnumerable<Path> GetPaths(Point start, Piece piece, IPieceEnumerationProvider squares)
            => piece.Type == PieceType.Knight
                ? squares.EnumerateKnight(start).Select(sq => new Path(GetType().Name)
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
