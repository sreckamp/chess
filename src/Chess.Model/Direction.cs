using System;
using System.Drawing;
using System.Linq;

namespace Chess.Model
{
    public enum Direction
    {
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
        public static readonly Direction[] All = (Direction[])Enum.GetValues(typeof(Direction));
        private static readonly Direction[] SDiagonals = {
            Direction.NorthEast, Direction.SouthEast,
            Direction.SouthWest, Direction.NorthWest
        };
        private static readonly Direction[] SCardinals = {
            Direction.North, Direction.East,
            Direction.South, Direction.West
        };

        public static bool IsCardinal(this Direction dir) => SCardinals.Contains(dir);

        public static bool IsDiagonal(this Direction dir) => SDiagonals.Contains(dir);

        public static Direction Opposite(this Direction dir)
        {
            return dir.RotateClockwise(All.Length/2);
        }

        public static Direction RotateClockwise(this Direction dir, int count = 1)
        {
            return (Direction) (((int) dir + count) % All.Length);
        }

        public static Direction RotateCounterClockwise(this Direction dir, int count = 1)
        {
            var idx = (int) dir - count;
            while (idx < 0) idx += All.Length;
            return (Direction) (idx % All.Length);
        }
    }
}
