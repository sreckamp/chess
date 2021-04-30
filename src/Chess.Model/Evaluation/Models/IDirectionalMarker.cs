using Chess.Model.Models;

namespace Chess.Model.Evaluation.Models
{
    public interface IDirectionalMarker : IMarker
    {
        Direction Direction { get; }
    }
}