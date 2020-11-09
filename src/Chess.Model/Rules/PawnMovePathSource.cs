using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Chess.Model.Models;
using Chess.Model.Models.Board;

namespace Chess.Model.Rules
{
    public sealed class PawnMovePathSource : IPathSource
    {
        /// <inheritdoc/>
        public IEnumerable<Path> GetPaths(Point start, Piece piece, IPieceEnumerationProvider squares)
        {
            return piece.Type == PieceType.Pawn
                ? new[] {piece.Edge.Opposite()}
                    .Where(direction => squares.EnumerateStraightLine(start, direction).Any())
                    .Select(direction => new Path
                    {
                        AllowMove = true,
                        AllowTake = false,
                        Direction = direction,
                        Start = start,
                        Piece = piece,
                        Squares = squares.EnumerateStraightLine(start, direction)
                            .Take(piece.HasMoved ? 1 : 2)
                            // .Select((sq, idx) => idx == 0
                            //     ? sq.Edges.Contains(square.Piece.Edge.Opposite())
                            //         ? (IMove)new PawnPromotionMove
                            //             {Piece = square.Piece, From = square.Location, To = sq.Location}
                            //         : new SimpleMove {Piece = square.Piece, From = square.Location, To = sq.Location}
                            //     : new PawnOpenMove {Piece = square.Piece, From = square.Location, To = sq.Location})
                    })
                : Enumerable.Empty<Path>();
        }
    }
}