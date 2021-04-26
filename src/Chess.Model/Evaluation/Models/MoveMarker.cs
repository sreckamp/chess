using System.Drawing;
using Chess.Model.Move;

namespace Chess.Model.Evaluation.Models
{
    public class MoveMarker:IMarker
    {
        public MoveMarker(IMove move)
        {
            Source = move.From;
            Move = move;
        }

        public MarkerType Type => MarkerType.Move;
        public Point Source { get; }
        public IMove Move { get; }
        public IMarker Clone() => new MoveMarker(Move);
    }
}
