using System.Linq;
using Chess.Model.Evaluation.Models;
using Chess.Model.Models;

namespace Chess.Model.Evaluation.Rules
{
    /// <summary>
    /// Prevents a pinned piece from moving out of the pin
    /// </summary>
    public sealed class PinMovePathRule : AbstractPathRule
    {
        public PinMovePathRule(IPathRule chain): base(chain) { }

        /// <inheritdoc />
        public override void Apply(IMarkingsProvider markings, Path path)
        {
            if (path.Squares.Any())
            {
                var pin = markings.GetMarkers<SimpleMarker>(path.Start, MarkerType.Pin).FirstOrDefault();

                if (pin != default)
                {
                    if (path.Direction != pin.Direction && path.Direction != pin.Direction.Opposite())
                    {
                        return;
                    }
                }
            }
            base.Apply(markings, path);
        }
    }
}