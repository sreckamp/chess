namespace Chess.Model.Models.Board
{
    public sealed class SimpleMarker : ISquareMarker
    {
        public SimpleMarker(MarkerType type, Square source, Direction direction)
        {
            Type = type;
            Source = source;
            Direction = direction;
        }
        public MarkerType Type { get; }
        public Square Source { get; }
        public Direction Direction { get; }
        
        public ISquareMarker Clone(ISquareProvider squares) => new SimpleMarker(
            Type,
            squares.GetSquare(Source.Location.X, Source.Location.Y),
            Direction);
    }
}