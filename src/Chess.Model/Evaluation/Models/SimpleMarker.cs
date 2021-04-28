using System.Drawing;
using Chess.Model.Models;

namespace Chess.Model.Evaluation.Models
{
    public class SimpleMarker : IDirectionalMarker
    {
        public SimpleMarker(MarkerType type, Point source, Piece piece, Direction direction)
        {
            Type = type;
            Source = source;
            Piece = piece;
            Direction = direction;
        }

        public MarkerType Type { get; }
        public Point Source { get; }
        public Piece Piece { get; }
        public Direction Direction { get; }

        public virtual IMarker Clone() => new SimpleMarker(Type, Source, Piece, Direction);
    }
}