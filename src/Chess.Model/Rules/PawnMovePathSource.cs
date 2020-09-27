using System.Collections.Generic;
using System.Linq;
using Chess.Model.Models;
using Chess.Model.Models.Board;
using Chess.Model.Move;

namespace Chess.Model.Rules
{
    public sealed class PawnMovePathSource : IPathSource
    {
        public bool Applies(PieceType type) => type == PieceType.Pawn;

        public IEnumerable<Path> GetPaths(Square square, ISquareProvider squares)
        {
            return Applies(square.Piece.Type)
                ? new[] {square.Piece.Edge.Opposite()}
                    .Where(direction => squares.EnumerateStraightLine(square.Location, direction).Any())
                    .Select(direction => new Path
                    {
                        AllowMove = true,
                        AllowTake = false,
                        Direction = direction,
                        Moves = squares.EnumerateStraightLine(square.Location, direction)
                            .Take(square.Piece.HasMoved ? 1 : 2)
                            .Select((sq, idx) => idx == 0
                                ? sq.Edges.Contains(square.Piece.Edge.Opposite())
                                    ? (IMove)new PawnPromotionMove
                                        {Piece = square.Piece, From = square.Location, To = sq.Location}
                                    : new SimpleMove {Piece = square.Piece, From = square.Location, To = sq.Location}
                                : new PawnOpenMove {Piece = square.Piece, From = square.Location, To = sq.Location})
                    })
                : Enumerable.Empty<Path>();
        }
    }
}