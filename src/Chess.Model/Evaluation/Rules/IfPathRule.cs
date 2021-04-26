using System;
using Chess.Model.Evaluation.Models;
using Chess.Model.Models.Board;

namespace Chess.Model.Evaluation.Rules
{
    public class IfPathRule: AbstractPathRule
    {
        private readonly Predicate<Path> m_predicate;
        private readonly IPathRule m_trueRule;

        /// <inheritdoc />
        public IfPathRule(Predicate<Path> predicate, IPathRule trueRule, IPathRule chain) : base(chain)
        {
            m_predicate = predicate;
            m_trueRule = trueRule;
        }

        /// <inheritdoc />
        public override void Apply(IMarkingsProvider markings, Path item)
        {
            if (m_predicate.Invoke(item))
            {
                m_trueRule.Apply(markings, item);
            }
            base.Apply(markings, item);
        }
    }
}