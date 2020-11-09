using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Chess.Model.Models;
using Chess.Model.Models.Board;

namespace Chess.Model.Rules
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
                    .Select(direction => new Path
                    {
                        AllowMove = false,
                        AllowTake = true,
                        Direction = direction,
                        Start = start,
                        Piece = piece,
                        Squares = squares.EnumerateStraightLine(start, direction).Take(1)
                            // .Select(sq => sq.Edges.Contains(square.Piece.Edge.Opposite())
                            //     ? (IMove) new PawnPromotionMove
                            //         {Piece = square.Piece, From = square.Location, To = sq.Location}
                            //     : new SimpleMove
                            //         {Piece = square.Piece, From = square.Location, To = sq.Location})
                    })
                : Enumerable.Empty<Path>();
    }
}