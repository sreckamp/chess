using System;
using Chess.Model.Models.Board;

namespace Chess.Model.Rules
{
    public sealed class NopPathRule : IPathRule
    {
        private static Lazy<IPathRule> m_lazyInstance = new Lazy<IPathRule>(() => new NopPathRule());

        public static IPathRule Instance => m_lazyInstance.Value;

        private NopPathRule()
        {
            
        }

        /// <inheritdoc />
        public void Apply(Square start, Path path, ISquareProvider squares)
        {
        }
    }
}