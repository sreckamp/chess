using System.Linq;
using Chess.Model.Models;
using Chess.Model.Models.Board;
using Chess.Model.Rules;

namespace Chess.Model.Evaluation.Rules
{
    public sealed class PinPathRule : AbstractPathRule
    {
        public PinPathRule(IPathRule chain): base(chain) { }

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