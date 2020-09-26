using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Chess.Model.Models;
using Chess.Model.Models.Board;
using Chess.Model.Move;
using static Chess.Model.Extensions.PointExtension;

namespace Chess.Model.Rules
{
    public class KingPathSource : IPathSource
    {
        public bool Applies(PieceType type) => type == PieceType.King;

        // TODO: In Check?
        // TODO: Ensure that available moves get out of check
        // TODO: Side.KingLocation or KingSquare & GetChecks that returns the attack directions.  Can block.  It attack is "none" must move king or take attacking piece.

        public IEnumerable<Path> GetPaths(Square square, ISquareProvider squares)
            => Applies(square.Piece.Type)
                ? Directions.All.Select(direction => new Path
                    {
                        AllowMove = true,
                        AllowTake = true,
                        Direction = direction,
                        Moves = squares.EnumerateStraightLine(square.Location, direction).Take(1)
                            .Select(sqr => new SimpleMove
                            {
                                Piece = square.Piece,
                                From = square.Location,
                                To = sqr.Location
                            }).Cast<IMove>()
                    }).Where(path => path.Moves != null && path.Moves.Any())
                : Enumerable.Empty<Path>();
    }
}
