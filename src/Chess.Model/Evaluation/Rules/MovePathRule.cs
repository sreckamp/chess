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
                if (path.AllowMove)
                {
                    markings.Mark(path.Start, path.Squares.TakeWhile(square => square.Item2.IsEmpty)
                        .Select((target, idx) => new MoveMarker(
                            GetMove(new SimpleMove(path.Start, target.Item1), idx))).ToArray()
                    );
                }

                if (path.AllowTake)
                {
                    markings.Mark(path.Start, path.Squares.SkipWhile((square) => path.AllowMove && square.Item2.IsEmpty)
                        .TakeWhile((square, idx) => !square.Item2.IsEmpty && square.Item2.Color != path.Piece.Color
                        ).Take(1)
                        .Select((target, idx) => new MoveMarker(
                            GetMove(new SimpleMove(path.Start, target.Item1), idx))).ToArray()
                    );
                }
            }

            if(Continue(path))
            {
                base.Apply(markings, path);
            }
        }

        protected virtual bool Applies(Path path) => path.Squares.Any();

        protected virtual bool Continue(Path path) => true;
        
        protected virtual IMove GetMove(SimpleMove move, int index) => move;
    }
}
