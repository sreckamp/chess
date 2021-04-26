using System;
using System.Linq;
using Chess.Model.Models.Board;
using Chess.Model.Move;

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
                var available = path.Squares.TakeWhile(square => square.Item2.IsEmpty).ToList();
                if(available.Count > 0)
                {
                    Console.WriteLine($"Move: {path}[{available.Count()}]");
                }
                markings.Mark(path.Start, available
                    .Select(target => new MoveMarker(new SimpleMove(path.Start, target.Item1))));
            }

            m_chain.Apply(markings, path);
        }
    }
}