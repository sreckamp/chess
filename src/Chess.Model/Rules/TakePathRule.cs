using System.Linq;
using Chess.Model.Models.Board;
using Chess.Model.Move;

namespace Chess.Model.Rules
{
    public sealed class TakePathRule : IPathRule
    {
        private readonly IPathRule m_chain;

        public TakePathRule(IPathRule chain)
        {
            m_chain = chain;
        }

        public void Apply(IMarkingsProvider markings, Path path)
        {
            if (path.AllowTake)
            {
                markings.Mark(path.Start, path.Squares.SkipWhile(square => square.Item2.IsEmpty)
                    .TakeWhile(target => target.Item2.Color != path.Piece.Color).Take(1).Select(
                        target => new MoveMarker(new SimpleMove(path.Start, target.Item1))));
            }

            m_chain.Apply(markings, path);
        }
    }
}