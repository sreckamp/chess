using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Chess.Model.Move;

namespace Chess.Model.Models.Board
{
    public sealed class Square
    {
        private readonly List<ISquareMarker> m_markers = new List<ISquareMarker>();

        public Point Location { get; set; }

        public IEnumerable<Direction> Edges { get; set; }

        public Piece Piece { get; set; }

        public bool IsEmpty => Piece.IsEmpty;

        /// <summary>
        /// Moves available for this square.
        /// </summary>
        public IEnumerable<IMove> Available { get; set; } = Enumerable.Empty<IMove>();

        public void Mark(ISquareMarker marker)
        {
            m_markers.Add(marker);
        }

        public IEnumerable<T> GetMarkers<T>() where T : ISquareMarker =>
            m_markers.Where(marker => marker is T).Cast<T>();

        public IEnumerable<ISquareMarker> GetMarkers(MarkerType type) =>
            m_markers.Where(marker => marker.Type == type);
    }
}