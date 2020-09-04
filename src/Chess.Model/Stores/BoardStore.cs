using System.Collections.Generic;
using System.Drawing;
using Chess.Model.Models;
using GameBase.Model;
using Color = Chess.Model.Models.Color;

namespace Chess.Model
{
    public class BoardStore
    {
        public int CornerSize { get; set; }
        public int Size { get; set; }
        public Point Selection { get; set; }
        public IEnumerable<Point> Available { get; set; }
        public IEnumerable<Placement<Piece>> Placements { get; set; }
        public Dictionary<Color, IEnumerable<Piece>> Captured { get; set; }
        /// <summary>
        /// A map of the state and the moves applied.
        /// </summary>
        public IEnumerable<MoveHistoryItem> History { get; set; }
    }
}
