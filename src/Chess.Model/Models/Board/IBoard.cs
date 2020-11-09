using System.Drawing;

namespace Chess.Model.Models.Board
{
    public interface IBoard
    {
        Piece this[Point p] { get; set; }
    }
}
