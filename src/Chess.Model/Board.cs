using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using GameBase.Model;

namespace Chess.Model
{
    public class Board : Board<Piece>
    {
        private const int ROWS = 8;
        private const int COLUMNS = 8;

        public Board(int cornerSize = 0) : base(Piece.CreateEmpty(), COLUMNS + 2 * cornerSize, ROWS + 2 * cornerSize)
        {
            CornerSize = cornerSize;
        }

        public int Height => ROWS + 2 * CornerSize;
        public int Width => COLUMNS + 2 * CornerSize;

        public int CornerSize { get; }

        /// <inheritdoc />
        public override void Clear()
        {
            base.Clear();
            for (var y = 0; y < Height; y++)
            {
                for (var x = 0; x < Width; x++)
                {
                    var pt = new Point(x, y);
                    if(!IsOnBoard(pt)) continue;

                    var m = new Move(pt);
                }
            }
        }

        private bool IsOnBoard(Point pt)
        {
            var isXCorner = pt.X < CornerSize || pt.X >= Height - CornerSize;
            var isYCorner = pt.Y < CornerSize || pt.Y >= Width - CornerSize;
            return !(isXCorner && isYCorner) && (pt.X >= 0 && pt.X < Width)
                   && (pt.Y >= 0 && pt.Y < Height);
        }

        public IEnumerable<Point> GetPossibleMoves(Color color, Point point)
        {
            var pc = this[point];
            if(pc.IsEmpty || pc.Color != color) return Enumerable.Empty<Point>();
            var result = new List<Point>();
            foreach (var direction in Directions.All)
            {
                var rule = pc.MoveRules[direction];
                for (var d = rule.MinCount; d > 0 && d <= rule.MaxCount; d++)
                {
                    var target = rule.GetResult(point, direction, d);
                    if (!(IsOnBoard(target) && this[target].IsEmpty))
                    {
                        break;
                    }
                    result.Add(target);
                }
                rule = pc.AttackRules[direction];
                for (var d = rule.MinCount; d > 0 && d <= rule.MaxCount; d++)
                {
                    var target = rule.GetResult(point, direction, d);
                    if (!IsOnBoard(target) || this[target].Color.Equals(pc.Color)) break;

                    if(result.Contains(target) || this[target].IsEmpty) continue;

                    result.Add(target);

                    break;
                }
            }

            return result;
        }

        // protected override void AddAvailableLocations(Placement<Piece> p)
        // {
        //     if("blank".Equals(p?.Piece?.Name))
        //     {
        //         AvailableLocations.Add(p.Move.Location);
        //     }
        // }

        public Piece Move(Point @from, Point to)
        {
            var source = Find(@from);
            var target = Find(to);

            var p = target.Piece;

            source.Location = target.Location;
            source.Piece.Moved();

            if (Placements.Contains(target))
            {
                Placements.Remove(target);
            }

            return p;
        }

        private Placement<Piece> Find(Point location)
        {
            try
            {
                return Placements.First(p => p.Location == location);
            }
            catch
            {
                return new Placement<Piece>(Piece.CreateEmpty(), location);
            }
        }
    }
}