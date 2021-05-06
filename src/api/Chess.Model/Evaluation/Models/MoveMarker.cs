using System.Drawing;
using Chess.Model.Models;
using Chess.Model.Move;

namespace Chess.Model.Evaluation.Models
{
    public class MoveMarker:IDirectionalMarker
    {
        public MoveMarker(IMove move, Direction direction)
        {
            Source = move.From;
            Move = move;
            Direction = direction;
        }

        /// <inheritdoc />
        public Direction Direction { get; }
        public MarkerType Type => MarkerType.Move;
        public Point Source { get; }
        public IMove Move { get; }
        public IMarker Clone() => new MoveMarker(Move, Direction);
    }
}
