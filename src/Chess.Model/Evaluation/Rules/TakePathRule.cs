using System.Linq;
using Chess.Model.Models.Board;
using Chess.Model.Move;
using Chess.Model.Rules;

namespace Chess.Model.Evaluation.Rules
{
    public sealed class TakePathRule : AbstractPathRule
    {
        public TakePathRule(IPathRule chain): base(chain) { }

        /// <inheritdoc/>
        public override void Apply(IMarkingsProvider markings, Path path)
        {
            if (path.AllowTake)
            {
                markings.Mark(path.Start, path.Squares.SkipWhile(square => square.Item2.IsEmpty)
                    .TakeWhile(target => target.Item2.Color != path.Piece.Color).Take(1).Select(
                        target => new MoveMarker(new SimpleMove(path.Start, target.Item1))));
            }

            base.Apply(markings, path);
        }
    }
}