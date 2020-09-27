using System.Collections.Generic;
using System.Linq;
using Chess.Model.Models;
using Chess.Model.Models.Board;
using Chess.Model.Move;

namespace Chess.Model.Rules
{
    public sealed class PawnTakePathSource : IPathSource
    {
        public bool Applies(PieceType type) => type == PieceType.Pawn;

        public IEnumerable<Path> GetPaths(Square square, ISquareProvider squares)
            => Applies(square.Piece.Type)
                ? new[]
                    {
                        square.Piece.Edge.RotateCounterClockwise(3),
                        square.Piece.Edge.RotateClockwise(3)
                    }
                    .Where(direction => squares.EnumerateStraightLine(square.Location, direction).Any())
                    .Select(direction => new Path
                    {
                        AllowMove = false,
                        AllowTake = true,
                        Direction = direction,
                        Moves = squares.EnumerateStraightLine(square.Location, direction).Take(1)
                            .Select(sq => sq.Edges.Contains(square.Piece.Edge.Opposite())
                                ? (IMove) new PawnPromotionMove
                                    {Piece = square.Piece, From = square.Location, To = sq.Location}
                                : new SimpleMove
                                    {Piece = square.Piece, From = square.Location, To = sq.Location})
                    })
                : Enumerable.Empty<Path>();
    }
}