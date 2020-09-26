using System.Collections.Generic;
using System.Linq;
using Chess.Model.Models;
using Chess.Model.Models.Board;
using Chess.Model.Move;
using static Chess.Model.Extensions.PointExtension;

namespace Chess.Model.Rules
{
    public class CastlePathSource : IPathSource
    {
        public bool Applies(PieceType type) => type == PieceType.King;

        public IEnumerable<Path> GetPaths(Square square, ISquareProvider squares)
        {
            var paths = new List<Path>();

            if(square.Piece.HasMoved 
               || !Applies(square.Piece.Type)
               || square.GetMarkers(MarkerType.Cover).Any(marker => marker.Source.Piece.Color != square.Piece.Color))
                return Enumerable.Empty<Path>();

            foreach (var rankDir in Directions.Cardinals.Where(dir => dir.IsPerpendicular(square.Piece.Edge)))
            {
                var castleSquares = squares.EnumerateStraightLine(square.Location, rankDir)
                    .TakeWhile(sqr => sqr.IsEmpty ||
                                      (sqr.Piece.Type == PieceType.Rook && sqr.Piece.Color == square.Piece.Color)).ToList();

                if (castleSquares.TakeWhile(sqr =>
                        sqr.GetMarkers(MarkerType.Cover).All(marker => marker.Source.Piece.Color == square.Piece.Color) && sqr.IsEmpty).Count() < 2) continue;

                Square rookSq;
                if ((rookSq = castleSquares.Last()).Piece.Type == PieceType.Rook && !castleSquares.Last().Piece.HasMoved)
                {
                    paths.Add(new Path
                    {
                        AllowMove = true,
                        Direction = Direction.None,
                        Moves = new[]
                        {
                            new CastleMove
                            {
                                Piece = square.Piece,
                                From = square.Location,
                                To = square.Location.PolarOffset(rankDir, 2),
                                RookMove = new SimpleMove
                                {
                                    Piece = rookSq.Piece, From = rookSq.Location,
                                    To = square.Location.PolarOffset(rankDir, 1)
                                }
                            }
                        }
                    });
                }
            }
            return paths;
        }
    }
}
