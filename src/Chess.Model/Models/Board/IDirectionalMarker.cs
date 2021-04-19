namespace Chess.Model.Models.Board
{
    public interface IDirectionalMarker : IMarker
    {
        Direction Direction { get; }
    }
}