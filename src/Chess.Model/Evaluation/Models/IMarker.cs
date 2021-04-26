using System.Drawing;

namespace Chess.Model.Models.Board
{
    public interface IMarker
    {
        /// <summary>
        /// The type of Marker represented
        /// </summary>
        MarkerType Type { get; }

        /// <summary>
        /// The location of the source of the marker
        /// </summary>
        Point Source { get; }

        /// <summary>
        /// Duplicates the marker
        /// </summary>
        /// <returns></returns>
        IMarker Clone();
    }
}