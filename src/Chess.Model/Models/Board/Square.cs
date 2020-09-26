using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Chess.Model.Move;

namespace Chess.Model.Models.Board
{
    public class Square
    {
        private readonly List<ISquareMarker> m_markers = new List<ISquareMarker>();

        public Point Location { get; set; }

        // public int X { get; set; }
        // public int Y { get; set; }
        public Piece Piece { get; set; }

        public bool IsEmpty => Piece.IsEmpty;

        public IEnumerable<IMove> Available { get; set; } = Enumerable.Empty<IMove>();

        public void Mark(ISquareMarker marker)
        {
            m_markers.Add(marker);
        }

        public IEnumerable<T> GetMarkers<T>() where T : ISquareMarker =>
            m_markers.Where(marker => marker is T).Cast<T>();

        public IEnumerable<ISquareMarker> GetMarkers(MarkerType type) =>
            m_markers.Where(marker => marker.Type == type);

        // private IEnumerable<IMove> GetAvailable(GameBoard board)
        // {
        //     // var location = new Point(X, Y);
        //     var result = new List<IMove>();
        //     //
        //     // var testDirs = new List<Direction>();
        //     // if (PinDir != Direction.None)
        //     // {
        //     //     testDirs.Add(PinDir);
        //     //     testDirs.Add(PinDir.Opposite());
        //     // }
        //     // else
        //     // {
        //     //     testDirs.AddRange(Directions.All);
        //     // }
        //     // foreach (var direction in testDirs)
        //     // {
        //     //     var rule = Piece.MoveRules[direction];
        //     //
        //     //     for (var d = rule.MinCount; d > 0 && d <= rule.MaxCount; d++)
        //     //     {
        //     //         var to = rule.GetResult(location, direction, d);
        //     //
        //     //         if(!board.IsOnBoard(to)) break;
        //     //
        //     //         var target= board[to.X, to.Y];
        //     //
        //     //         if (!target.IsEmpty) break;
        //     //
        //     //         result.Add(to);
        //     //     }
        //     //
        //     //     rule = Piece.AttackRules[direction];
        //     //     for (var d = rule.MinCount; d > 0 && d <= rule.MaxCount; d++)
        //     //     {
        //     //         var to = rule.GetResult(location, direction, d);
        //     //
        //     //         if(!board.IsOnBoard(to)) break;
        //     //
        //     //         var target= board.GetSquare(to.Y,to.X);
        //     //
        //     //         var attackDir = rule.MaxCount == 1 ? Direction.None : direction;
        //     //
        //     //         if (!target.AttackedBy.ContainsKey(attackDir))
        //     //         {
        //     //             target.AttackedBy[attackDir] = new HashSet<Color>();
        //     //         }
        //     //
        //     //         target.AttackedBy[attackDir].Add(Piece.Color);
        //     //
        //     //         if (target.Piece.Color.Equals(Piece.Color)) break;
        //     //
        //     //         if (attackDir != Direction.None)
        //     //         {
        //     //             for (var pinPoint = to.Offset(attackDir, 1);
        //     //                 board.IsOnBoard(pinPoint);
        //     //                 pinPoint = pinPoint.Offset(attackDir, 1))
        //     //             {
        //     //                 var pinTest = board[pinPoint.X, pinPoint.Y];
        //     //
        //     //                 if (pinTest.IsEmpty) continue;
        //     //
        //     //                 if (pinTest.Type != PieceType.King || pinTest.Color != target.Piece.Color) break;
        //     //
        //     //                 target.PinDir = attackDir;
        //     //                 target.Update(board);
        //     //                 break;
        //     //             }
        //     //         }
        //     //
        //     //         if(result.Contains(to) || target.IsEmpty) continue;
        //     //
        //     //         result.Add(to);
        //     //
        //     //         break;
        //     //     }
        //     // }
        //
        //     return result;
        // }
    }
}