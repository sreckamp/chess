using Chess.Model.Evaluation.Models;
using Chess.Model.Models.Board;

namespace Chess.Model.Evaluation.Rules
{
    public abstract class AbstractPathRule : IPathRule
    {
        private readonly IPathRule m_chain;
        public AbstractPathRule(IPathRule chain)
        {
            m_chain = chain;
        }

        /// <inheritdoc />
        public virtual void Apply(IMarkingsProvider markings, Path item)
        {
            m_chain.Apply(markings, item);
        }
    }
}