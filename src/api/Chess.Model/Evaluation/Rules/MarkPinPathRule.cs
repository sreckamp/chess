using System.Drawing;
using System.Linq;
using Chess.Model.Evaluation.Models;
using Chess.Model.Models;

namespace Chess.Model.Evaluation.Rules
{
    /// <summary>
    /// Rule used to detect and mark pins
    /// TODO: Unit tests
    /// </summary>
    public sealed class MarkPinPathRule : AbstractPathRule
    {
        public MarkPinPathRule(IPathRule chain): base(chain) { }

        /// <inheritdoc />
        public override void Apply(IMarkingsProvider markings, Path path)
        {
            if (path.Squares.Any())
            {
                var pinned = (new Point(-1,-1), Piece.Empty);

                foreach (var target in path.Squares.Where(target => !target.Item2.IsEmpty))
                {
                    if (pinned.Item2.IsEmpty)
                    {
                        pinned = target;
                        continue;
                    }

                    if (target.Item2.Type != PieceType.King ||
                        target.Item2.Color != pinned.Item2.Color ||
                        path.Piece.Color == target.Item2.Color) break;


                    markings.Mark(pinned.Item1, new SimpleMarker(MarkerType.Pin, path.Start, path.Piece, path.Direction));

                    break;
                }
            }

            base.Apply(markings, path);
        }
    }
}