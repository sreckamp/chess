using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Chess.Model.Evaluation.Models;
using Chess.Model.Models;

namespace Chess.Model.Evaluation.Sources
{
    public sealed class PawnTakePathSource : IPathSource
    {
        /// <inheritdoc/>
        public IEnumerable<Path> GetPaths(Point start, Piece piece, IPieceEnumerationProvider squares)
            => piece.Type == PieceType.Pawn
                ? new[]
                    {
                        piece.Edge.RotateCounterClockwise(3),
                        piece.Edge.RotateClockwise(3)
                    }
                    .Where(direction => squares.EnumerateStraightLine(start, direction).Any())
                    .Select(direction => new Path(GetType().Name)
                    {
                        AllowMove = false,
                        AllowTake = true,
                        Direction = direction,
                        Start = start,
                        Piece = piece,
                        OppositeEdge = !squares.EnumerateStraightLine(start, piece.Edge.Opposite()).Skip(1).Any(),
                        Squares = squares.EnumerateStraightLine(start, direction).Take(1)
                    })
                : Enumerable.Empty<Path>();
    }
}
