using System.Linq;
using Chess.Model.Models.Board;

namespace Chess.Model.Rules
{
    public class TakePathRule : IPathRule
    {
        private readonly IPathRule m_chain;
        public TakePathRule(IPathRule chain)
        {
            m_chain = chain;
        }

        public void Apply(Square start, Path path, ISquareProvider squares)
        {
            if (path.Moves.Any() && path.AllowTake)
            {
                start.Available = start.Available.Union(
                    path.Moves.SkipWhile(move => squares.GetSquare(move.To).IsEmpty)
                        .TakeWhile(move => squares.GetSquare(move.To).Piece.Color != start.Piece.Color).Take(1));
            }

            m_chain.Apply(start, path, squares);
        }
    }
}