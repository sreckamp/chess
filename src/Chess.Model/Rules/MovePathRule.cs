using System.Linq;
using Chess.Model.Models.Board;
using Chess.Model.Move;
using Chess.Model.Stores;

namespace Chess.Model.Rules
{
    public sealed class MovePathRule : IPathRule
    {
        private readonly IPathRule m_chain;

        public MovePathRule(IPathRule chain)
        {
            m_chain = chain;
        }

        /// <inheritdoc />
        public void Apply(IMarkingsProvider markings, Path path)
        {
            if (path.Squares.Any() && path.AllowMove)
            {
                markings.Mark(path.Start, path.Squares.TakeWhile(square => square.Item2.IsEmpty)
                    .Select(target => new MoveMarker(new SimpleMove(path.Start, target.Item1))));
            }

            m_chain.Apply(markings, path);
        }
    }
}