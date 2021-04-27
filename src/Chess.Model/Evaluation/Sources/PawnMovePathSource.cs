using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Chess.Model.Evaluation.Models;
using Chess.Model.Models;

namespace Chess.Model.Evaluation.Sources
{
    public sealed class PawnMovePathSource : IPathSource
    {
        /// <inheritdoc/>
        public IEnumerable<Path> GetPaths(Point start, Piece piece, IPieceEnumerationProvider squares)
        {
            return piece.Type == PieceType.Pawn
                ? new[] {piece.Edge.Opposite()}
                    .Where(direction => squares.EnumerateStraightLine(start, direction).Any())
                    .Select(direction =>
                    {
                        var moves = squares.EnumerateStraightLine(start, direction).Take(2).ToList();

                        return new Path(GetType().Name)
                        {
                            AllowMove = true,
                            AllowTake = false,
                            Direction = direction,
                            Start = start,
                            Piece = piece,
                            OppositeEdge = moves.Count() == 1,
                            Squares = moves.Take(piece.HasMoved ? 1 : 2)
                        };
                    })
                : Enumerable.Empty<Path>();
        }
    }
}