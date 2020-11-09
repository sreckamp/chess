using System.Drawing;

namespace Chess.Model.Models.Board
{
    public sealed class SimpleMarker : IMarker
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

        public IMarker Clone() => new SimpleMarker(Type, Source, Piece, Direction);
    }
}