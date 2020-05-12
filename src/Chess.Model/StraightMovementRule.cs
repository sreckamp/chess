using System.Drawing;

namespace Chess.Model
{
    public class StraightMovementRule : IMovementRule
    {
        public static readonly StraightMovementRule None = new StraightMovementRule(0, 0);
        public static readonly StraightMovementRule OneSpace = new StraightMovementRule(1, 1);
        public static readonly StraightMovementRule TwoSpace = new StraightMovementRule(2, 2);
        public static readonly StraightMovementRule Infinite = new StraightMovementRule(1, int.MaxValue);

        private StraightMovementRule(int minCount, int maxCount)
        {
            MinCount = minCount;
            MaxCount = maxCount;
        }

        /// <inheritdoc />
        public int MinCount { get; }

        /// <inheritdoc />
        public int MaxCount { get; }

        /// <inheritdoc />
        public Point GetResult(Point start, Direction dir, int count)
        {
            if (count < MinCount || count > MaxCount) return start;

            return start.Offset(dir, count);
        }
    }
}