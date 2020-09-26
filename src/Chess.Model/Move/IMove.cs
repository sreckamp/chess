using System.Drawing;
using Chess.Model.Models;

namespace Chess.Model.Move
{
    public interface IMove
    {
        Point From { get; }
        Point To { get; }
        Piece Apply(GameBoard board);

        IMove Clone();
    }
}