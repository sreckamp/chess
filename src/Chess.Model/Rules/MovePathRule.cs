using System.Linq;
using Chess.Model.Models.Board;

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
        public void Apply(Square start, Path path, ISquareProvider squares)
        {
            if (path.Moves.Any() && path.AllowMove)
            {
                start.Available = start.Available.Union(
                    path.Moves.TakeWhile(move => squares.GetSquare(move.To).IsEmpty));
            }

            m_chain.Apply(start, path, squares);
        }
    }
}