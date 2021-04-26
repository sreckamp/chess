using System;
using Chess.Model.Models.Board;

namespace Chess.Model.Rules
{
    public class FilterPathRule: IPathRule
    {
        private readonly IPathRule m_chain;
        private readonly Func<Path, bool> m_predicate;

        public FilterPathRule(Func<Path, bool> predicate, IPathRule chain)
        {
            m_chain = chain;
            m_predicate = predicate ?? (path => true);
        }

        public void Apply(IMarkingsProvider markings, Path path)
        {
            if (m_predicate.Invoke(path))
            {
                m_chain.Apply(markings, path);
            }
        }
    }
}