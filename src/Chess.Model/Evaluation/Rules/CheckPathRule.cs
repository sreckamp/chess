using System.Linq;
using Chess.Model.Evaluation.Models;
using Chess.Model.Models;

namespace Chess.Model.Evaluation.Rules
{
    /// <summary>
    /// Mark paths that include check
    /// </summary>
    public sealed class CheckPathRule : AbstractPathRule
    {
        public CheckPathRule(IPathRule chain): base(chain) { }

        /// <inheritdoc />
        public override void Apply(IMarkingsProvider markings, Path path)
        {
            if (path.Squares.Any())
            {
                var squares = path.Squares.ToList();
                CheckMarker marker = default;

                var i = 0;

                for (; i < squares.Count; i++)
                {
                    var (target, targetPiece) = squares[i];

                    if (targetPiece.IsEmpty) continue;

                    if (targetPiece.Color == path.Piece.Color ||
                        targetPiece.Type != PieceType.King) break;

                    markings.InCheck.Add(targetPiece.Color);

                    marker = new CheckMarker(path.Start, target, path.Direction);
                }

                if (marker != null)
                {
                    for (i--; i > -1; i--)
                    {
                        var (target, _) = squares[i];

                        markings.Mark(target, marker);
                    }

                    markings.Mark(path.Start, marker);
                }
            }

            base.Apply(markings, path);
        }
    }
}