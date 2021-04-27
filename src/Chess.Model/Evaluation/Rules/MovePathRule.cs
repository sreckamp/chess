using System.Linq;
using Chess.Model.Evaluation.Models;
using Chess.Model.Move;

namespace Chess.Model.Evaluation.Rules
{
    public class MovePathRule : AbstractPathRule
    {
        public MovePathRule(IPathRule chain) : base(chain) { }

        /// <inheritdoc />
        public override void Apply(IMarkingsProvider markings, Path path)
        {
            if (Applies(path))
            {
                markings.Mark(path.Start, path.Squares.TakeWhile(square => square.Item2.IsEmpty)
                    .Select((target, idx) => new MoveMarker(
                        GetMove(new SimpleMove(path.Start, target.Item1), idx)))
                );
            }

            if(Continue(path))
            {
                base.Apply(markings, path);
            }
        }

        protected virtual bool Applies(Path path) => path.Squares.Any() && path.AllowMove;

        protected virtual bool Continue(Path path) => true;
        
        protected virtual IMove GetMove(SimpleMove move, int index) => move;
    }
}
