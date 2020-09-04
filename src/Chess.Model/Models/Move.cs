using System.Drawing;

namespace Chess.Model.Models
{
    public class Move
    {
        public Piece Piece { get; set; }
        public Point From { get; set; }
        public Point To { get; set; }
    }
}