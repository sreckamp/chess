using System.Drawing;

namespace Chess.Model.Models
{
    public interface IBoard
    {
        Piece this[Point p] { get; set; }
    }
}
