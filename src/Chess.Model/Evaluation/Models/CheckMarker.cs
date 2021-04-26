using System.Drawing;
using Chess.Model.Models;

namespace Chess.Model.Evaluation.Models
{
    public sealed class CheckMarker : IDirectionalMarker
    {
        public CheckMarker(Point source, Point target, Direction direction)
        {
            Source = source;
            Target = target;
            Direction = direction;
        }

        public MarkerType Type => MarkerType.Check;
        public Point Source { get; }
        public Point Target { get; }
        public Direction Direction { get; }

        public IMarker Clone() => new CheckMarker(Source, Target, Direction);
    }
}