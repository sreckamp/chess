using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Chess.Model.Models;
using Chess.Model.Move;

namespace Chess.Model.Stores
{
    public class MovesStore
    {
        private Dictionary<Point, List<IMove>> m_available = new Dictionary<Point, List<IMove>>();

        /// <summary>
        /// Temporary state of the game board
        /// </summary>
        public GameBoard Board { get; set; }
        
        public IEnumerable<MoveHistoryItem> History { get; set; }

        public void AddMove(IMove move)
        {
            if (!m_available.ContainsKey(move.From))
            {
                m_available[move.From] = new List<IMove>();
            }

            m_available[move.From].Add(move);
        }

        public IEnumerable<IMove> GetAvailable(Point location)
            => m_available.ContainsKey(location)
                ? m_available[location]
                : Enumerable.Empty<IMove>();

        public MovesStore Clone() => new MovesStore
        {
            Board = Board,
            History = History.Select(item => item.Clone()).ToList(),
            m_available = m_available.ToDictionary(pair => pair.Key, pair => 
                pair.Value.ToList())
        };
    }
}