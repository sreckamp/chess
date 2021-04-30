using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Chess.Model.Evaluation.Models;
using Chess.Model.Models;

namespace Chess.Model.Evaluation.Sources
{
    public sealed class KingPathSource : IPathSource
    {
        /// <inheritdoc/>
        public IEnumerable<Path> GetPaths(Point start, Piece piece, IPieceEnumerationProvider squares)
            => piece.Type == PieceType.King
                ? Directions.All.Where(direction => squares.EnumerateStraightLine(start, direction).Any())
                    .Select(direction => new Path(GetType().Name)
                    {
                        AllowMove = true,
                        AllowTake = true,
                        Direction = direction,
                        Start = start,
                        Piece = piece,
                        Squares = squares.EnumerateStraightLine(start, direction).Take(1)
                    }).Concat(
                        !piece.HasMoved
                        ? Directions.Cardinals.Where(dir => dir.IsPerpendicular(piece.Edge))
                            .Select(direction => new Path(GetType().Name)
                                {
                                    AllowMove = true,
                                    Start = start,
                                    Piece = piece,
                                    Direction = direction,
                                    Squares = squares.EnumerateStraightLine(start, direction)
                                }
                            )
                        : Enumerable.Empty<Path>()
                    )
                : Enumerable.Empty<Path>();
        // TODO: if the king has not moved, include the horizontal paths for castling
    }
}
