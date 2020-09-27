namespace Chess.Model.Models.Board
{
    public sealed class CheckMarker : ISquareMarker
    {
        public CheckMarker(Square source, Square target, Direction direction)
        {
            Source = source;
            Target = target;
            Direction = direction;
        }

        public MarkerType Type => MarkerType.Check;
        public Square Source { get; }
        public Square Target { get; }
        public Direction Direction { get; }

        public ISquareMarker Clone(ISquareProvider squares) => new CheckMarker(
            squares.GetSquare(Source.Location.X, Source.Location.Y),
            squares.GetSquare(Target.Location.X, Target.Location.Y),
            Direction);
    }
}