﻿using System;
using System.Drawing;
using Chess.Model.Models;

namespace Chess.Model.Extensions
{
    public static class PointExtension
    {
        public static Point CartesianOffset(this Point pt, Point offset)
        {
            return new Point(pt.X + offset.X, pt.Y + offset.Y);
        }

        public static Point Divide(this Point pt, int divisor)
        {
            return new Point(pt.X / divisor, pt.Y / divisor);
        }

        public static Point PolarOffset(this Point pt, Direction dir, int count)
        {
            var xOffset = 0;
            var yOffset = 0;

            if(dir == Direction.NorthWest || dir == Direction.North || dir == Direction.NorthEast)
            {
                yOffset = count;
            }
            else if(dir == Direction.SouthWest || dir == Direction.South || dir == Direction.SouthEast)
            {
                yOffset = -count;
            }

            if(dir == Direction.NorthWest || dir == Direction.West || dir == Direction.SouthWest)
            {
                xOffset = -count;
            }
            else if(dir == Direction.NorthEast || dir == Direction.East || dir == Direction.SouthEast)
            {
                xOffset = count;
            }

            return new Point(pt.X + xOffset, pt.Y + yOffset);
        }

        public static bool IsBetween(this Point test, Point start, Point end)
        {
            var distTestStart = Distance(test, start);
            var distTestEnd = Distance(test, end);
            var distStartEnd = Distance(start, end);
            return Math.Abs(distTestStart + distTestEnd - distStartEnd) < 0.1;
        }

        private static double Distance(Point start, Point end)
        {
            return Math.Sqrt(Math.Pow(end.X - start.X, 2) + Math.Pow(end.Y - start.Y, 2));
        }
    }
}