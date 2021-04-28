using System.Drawing;
using Chess.Model.Models;

namespace Chess.Model.Evaluation.Models
{
    public sealed class CheckMarker : SimpleMarker
    {
        public CheckMarker(Point source, Piece piece, Point kingLocation, Direction direction)
            : base(MarkerType.Check, source, piece, direction)
        {
            KingLocation = kingLocation;
        }

        public Point KingLocation { get; }

        public override IMarker Clone() => new CheckMarker(Source, Piece, KingLocation, Direction);
    }
}