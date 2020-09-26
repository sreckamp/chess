using System.Collections.Generic;
using System.Linq;
using Chess.Model.Models;
using Chess.Model.Models.Board;
using Chess.Model.Move;

namespace Chess.Model.Rules
{
    public class DiagonalPathSource : IPathSource
    {
        public bool Applies(PieceType type) => type == PieceType.Bishop || type == PieceType.Queen;

        public IEnumerable<Path> GetPaths(Square square, ISquareProvider squares)
            => Applies(square.Piece.Type)
                ? Directions.Diagonals.Select(direction => new Path
                {
                    AllowMove = true,
                    AllowTake = true,
                    Direction = direction,
                    Moves = squares.EnumerateStraightLine(square.Location, direction)
                        .Select(sq => new SimpleMove
                            {Piece = square.Piece, From = square.Location, To = sq.Location})
                }) 
                : Enumerable.Empty<Path>();
    }
}