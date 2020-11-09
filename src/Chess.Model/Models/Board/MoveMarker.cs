using System.Drawing;
using Chess.Model.Move;

namespace Chess.Model.Models.Board
{
    public class MoveMarker:ISquareMarker
    {
        public MoveMarker(IMove move)
        {
            Source = move.From;
            Move = move;
        }

        public MarkerType Type => MarkerType.Move;
        public Point Source { get; }
        public IMove Move { get; }
        public ISquareMarker Clone()
        {
            throw new System.NotImplementedException();
        }
    }
}