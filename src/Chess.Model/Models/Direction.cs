using System;
using System.Collections.Generic;
using System.Linq;

namespace Chess.Model.Models
{
    public enum Direction
    {
        None,
        North,
        NorthEast,
        East,
        SouthEast,
        South,
        SouthWest,
        West,
        NorthWest,
    }

    public static class Directions
    {
        public static readonly Direction[] All = ((Direction[])Enum.GetValues(typeof(Direction)))
            .Where(d => d != Direction.None).ToArray();
        private static readonly Direction[] SDiagonals = {
            Direction.NorthEast, Direction.SouthEast,
            Direction.SouthWest, Direction.NorthWest
        };
        private static readonly Direction[] SCardinals = {
            Direction.North, Direction.East,
            Direction.South, Direction.West
        };
        private static readonly Direction[] SNorthSouth = {
            Direction.North, Direction.South
        };

        public static bool IsCardinal(this Direction dir) => dir.IsMember(SCardinals);

        public static bool IsDiagonal(this Direction dir) => dir.IsMember(SDiagonals);

        public static bool IsNorthSouth(this Direction dir) => dir.IsMember(SNorthSouth);

        public static bool IsMember(this Direction dir, IEnumerable<Direction> options) => options.Contains(dir);
        public static Direction Opposite(this Direction dir)
        {
            return dir.RotateClockwise(All.Length/2);
        }

        public static Direction RotateClockwise(this Direction dir, int count = 1)
        {
            if(dir == Direction.None) return Direction.None;

            var tmp = (int) dir + count - 1;
            tmp %= All.Length;
            tmp++;

            return (Direction) tmp;
        }

        public static Direction RotateCounterClockwise(this Direction dir, int count = 1)
        {
            if(dir == Direction.None) return Direction.None;

            var tmp = (int) dir - 1 - count;
            while (tmp < 0) tmp += All.Length;
            tmp %= All.Length;
            tmp++;
                
            return (Direction) tmp;
        }
    }
}
