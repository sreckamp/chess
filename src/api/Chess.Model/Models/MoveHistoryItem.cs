using System.Collections.Generic;
using System.Linq;
using Chess.Model.Move;

namespace Chess.Model.Models
{
    public struct MoveHistoryItem
    {
        public GameBoard Start { get; set; }

        public IMove Move { get; set; }
        
        public Piece Taken { get; set; }
        public IEnumerable<Color> Checks { get; set; }
        
        public MoveHistoryItem Clone() => new MoveHistoryItem
        {
            Start = Start,
            Move = Move,
            Taken = Taken,
            Checks = Checks.Select(color => color).ToList()
        };
    }
}
