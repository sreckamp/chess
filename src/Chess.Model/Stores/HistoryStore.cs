using System.Collections.Generic;
using Chess.Model.Models;

namespace Chess.Model.Stores
{
    public class HistoryStore
    {
        /// <summary>
        /// Temporary state of the game board
        /// </summary>
        public GameBoard Board { get; set; }

        public IEnumerable<MoveHistoryItem> History { get; set; }
    }
}