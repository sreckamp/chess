namespace Chess.Model.Models.Board
{
    public interface ISquareMarker
    {
        MarkerType Type { get; }
        Square Source { get; }
        Direction Direction { get; }
        ISquareMarker Clone(ISquareProvider squares);
    }
}