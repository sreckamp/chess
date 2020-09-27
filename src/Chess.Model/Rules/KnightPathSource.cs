using System.Collections.Generic;
using System.Linq;
using Chess.Model.Models;
using Chess.Model.Models.Board;
using Chess.Model.Move;

namespace Chess.Model.Rules
{
    public sealed class KnightPathSource : IPathSource
    {
        public bool Applies(PieceType type) => type == PieceType.Knight;

        public IEnumerable<Path> GetPaths(Square square, ISquareProvider squares)
            => Applies(square.Piece.Type)
                ? squares.EnumerateKnight(square.Location).Select(sq => new Path
                {
                    AllowMove = true,
                    AllowTake = true,
                    Direction = Direction.None,
                    Moves = new[]
                    {
                        new SimpleMove
                        {
                            Piece = square.Piece,
                            From = square.Location,
                            To = sq.Location
                        }
                    }
                })
                : Enumerable.Empty<Path>();
    }
}
