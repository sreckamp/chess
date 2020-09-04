using System.Collections.Generic;
using System.Drawing;
using Chess.Model.Stores;
using GameBase.Model;

namespace Chess.Model.Models
{
    public class MoveHistoryItem
    {
        public IEnumerable<Placement<Piece>> Start { get; set; }
        public Dictionary<Color, IEnumerable<Piece>> Captured { get; set; }
        public Move Move { get; set; }
    }
}
