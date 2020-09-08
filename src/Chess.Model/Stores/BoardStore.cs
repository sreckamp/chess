using System.Collections.Generic;
using System.Drawing;
using Chess.Model.Models;
using Color = Chess.Model.Models.Color;

namespace Chess.Model.Stores
{
    public class BoardStore
    {
        public Board Board { get; set; }
        public Dictionary<Color, IEnumerable<Piece>> Captured { get; set; }
        /// <summary>
        /// A map of the state and the moves applied.
        /// </summary>
        public IEnumerable<MoveHistoryItem> History { get; set; }
    }
}
