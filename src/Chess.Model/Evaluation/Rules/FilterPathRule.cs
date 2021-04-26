using System;
using Chess.Model.Evaluation.Models;
using Chess.Model.Models.Board;

namespace Chess.Model.Evaluation.Rules
{
    public class FilterPathRule: AbstractPathRule
    {
        private readonly Func<Path, bool> m_predicate;

        public FilterPathRule(Func<Path, bool> predicate, IPathRule chain): base(chain)
        {
            m_predicate = predicate ?? (path => true);
        }

        /// <inheritdoc />
        public override void Apply(IMarkingsProvider markings, Path path)
        {
            if (m_predicate.Invoke(path))
            {
                base.Apply(markings, path);
            }
        }
    }
}