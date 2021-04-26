using Chess.Model.Models.Board;
using Chess.Model.Rules;

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