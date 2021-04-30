// using System.Collections.Generic;
// using System.Drawing;
// using System.Linq;
// using Chess.Model.Evaluation.Models;
// using Chess.Model.Evaluation.Sources;
// using Chess.Model.Models;
// using Chess.Model.Move;
//
// namespace Chess.Model.Rules
// {
//     public sealed class CastlePathSource : IPathSource
//     {
//         public bool Applies(PieceType type) => type == PieceType.King;
//
//         public IEnumerable<Path> GetPaths(Point start, Piece piece, IPieceEnumerationProvider squares)
//         {
//             var paths = new List<Path>();
//
//             if(piece.HasMoved || !Applies(piece.Type)
//                || square.GetMarkers(MarkerType.Cover).Any(marker => marker.Source.Piece.Color != square.Piece.Color))
//                 return Enumerable.Empty<Path>();
//
//             foreach (var rankDir in Directions.Cardinals.Where(dir => dir.IsPerpendicular(square.Piece.Edge)))
//             {
//                 var castleSquares = squares.EnumerateStraightLine(square.Location, rankDir)
//                     .TakeWhile(sqr => sqr.IsEmpty ||
//                                       (sqr.Piece.Type == PieceType.Rook && sqr.Piece.Color == square.Piece.Color)).ToList();
//
//                 if (castleSquares.TakeWhile(sqr =>
//                         sqr.GetMarkers(MarkerType.Cover).All(marker => marker.Source.Piece.Color == square.Piece.Color) && sqr.IsEmpty).Count() < 2) continue;
//
//                 Square rookSq;
//                 if ((rookSq = castleSquares.Last()).Piece.Type == PieceType.Rook && !castleSquares.Last().Piece.HasMoved)
//                 {
//                     paths.Add(new Path
//                     {
//                         AllowMove = true,
//                         Direction = rankDir,
//                         Moves = new[]
//                         {
//                             new CastleMove
//                             {
//                                 Piece = piece,
//                                 From = square.Location,
//                                 To = square.Location.PolarOffset(rankDir, 2),
//                                 RookMove = new SimpleMove
//                                 {
//                                     Piece = rookSq.Piece,
//                                     From = rookSq.Location,
//                                     To = square.Location.PolarOffset(rankDir, 1)
//                                 }
//                             }
//                         }
//                     });
//                 }
//             }
//             return paths;
//         }
//     }
// }
