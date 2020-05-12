using System.Drawing;

namespace Chess.Model
{
    public class KnightMovementRule : IMovementRule
    {
        public static readonly KnightMovementRule Instance = new KnightMovementRule();

        private KnightMovementRule()
        {
        }

        /// <inheritdoc />
        public int MinCount => 1;

        /// <inheritdoc />
        public int MaxCount => 1;

        /// <inheritdoc />
        public Point GetResult(Point start, Direction dir, int count)
        {
            var xOffset = 0;
            var yOffset = 0;

            switch (dir)
            {
                case Direction.North:
                    xOffset = -1;
                    yOffset = -2;
                    break;
                case Direction.NorthEast:
                    xOffset = 1;
                    yOffset = -2;
                    break;
                case Direction.East:
                    xOffset = 2;
                    yOffset = -1;
                    break;
                case Direction.SouthEast:
                    xOffset = 2;
                    yOffset = 1;
                    break;
                case Direction.South:
                    xOffset = 1;
                    yOffset = -2;
                    break;
                case Direction.SouthWest:
                    xOffset = -1;
                    yOffset = 2;
                    break;
                case Direction.West:
                    xOffset = -2;
                    yOffset = 1;
                    break;
                case Direction.NorthWest:
                    xOffset = -2;
                    yOffset = -1;
                    break;
            }

            return new Point(start.X + xOffset, start.Y + yOffset);
        }
    }
}
