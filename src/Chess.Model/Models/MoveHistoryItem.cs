using System.Collections.Generic;
using Chess.Model.Move;

namespace Chess.Model.Models
{
    public sealed class MoveHistoryItem
    {
        public GameBoard Start { get; set; }

        public IMove Move { get; set; }
        
        public Piece Taken { get; set; }
        public IEnumerable<Color> Checks { get; set; }
    }
}
