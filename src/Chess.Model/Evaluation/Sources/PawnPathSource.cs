using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Chess.Model.Evaluation.Models;
using Chess.Model.Models;

namespace Chess.Model.Evaluation.Sources
{
    public sealed class PawnPathSource : IPathSource
    {
        /// <inheritdoc/>
        public IEnumerable<Path> GetPaths(Point start, Piece piece, IPieceEnumerationProvider squares)
        {
            return piece.Type == PieceType.Pawn
                ? new[]
                    {
                        piece.Edge.RotateCounterClockwise(3),
                        piece.Edge.Opposite(),
                        piece.Edge.RotateClockwise(3)
                    }
                    .Where(direction => squares.EnumerateStraightLine(start, direction).Any())
                    .Select(direction =>
                    {
                        var isMove = direction == piece.Edge.Opposite();
                        var moves = squares.EnumerateStraightLine(start, direction).Take(2).ToList();

                        return new Path(GetType().Name)
                        {
                            AllowMove = isMove,
                            AllowTake = !isMove,
                            Direction = direction,
                            Start = start,
                            Piece = piece,
                            OppositeEdge = !squares.EnumerateStraightLine(start, piece.Edge.Opposite()).Skip(1).Any(),
                            Squares = moves.Take(piece.HasMoved || !isMove ? 1 : 2)
                        };
                    })
                : Enumerable.Empty<Path>();
        }
    }
}