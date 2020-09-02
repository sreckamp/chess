using System.Drawing;

namespace Chess.Model
{
    public class StraightMovementRule : IMovementRule
    {
        public static readonly StraightMovementRule None = new StraightMovementRule(0);
        public static readonly StraightMovementRule OneSpace = new StraightMovementRule(1);
        public static readonly StraightMovementRule TwoSpace = new StraightMovementRule(2);
        public static readonly StraightMovementRule Infinite = new StraightMovementRule(int.MaxValue);

        private StraightMovementRule(int maxCount)
        {
            MinCount = maxCount > 0 ? 1 : 0;
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

        public override string ToString()
        {
            return MinCount == MaxCount 
                ? $"{MaxCount} Squares"
                : $"{MinCount}-{(MaxCount == int.MaxValue ? "" : $"{MaxCount}")} Squares";
        }
    }
}
