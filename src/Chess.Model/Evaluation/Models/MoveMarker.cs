using System.Drawing;
using Chess.Model.Move;

namespace Chess.Model.Models.Board
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
        public IMarker Clone()
        {
            throw new System.NotImplementedException();
        }
    }
}