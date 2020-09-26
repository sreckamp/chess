using System.Collections.Generic;
using System.Linq;
using Chess.Model.Models;
using Color = Chess.Model.Models.Color;

namespace Chess.Model.Stores
{
    public class GameStore
    {
        public Version Version { get; set; }
        public Color CurrentPlayer { get; set; }
        public GameBoard Board { get; set; }
        public IEnumerable<Side> Sides { get; set; }
        public IEnumerable<MoveHistoryItem> HistoryItems { get; set; }
    }
}
