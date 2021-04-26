using System;
using Chess.Model.Evaluation.Models;
using Chess.Model.Models.Board;

namespace Chess.Model.Evaluation.Rules
{
    public sealed class NopPathRule : IPathRule
    {
        private static readonly Lazy<IPathRule> LazyInstance = new Lazy<IPathRule>(() => new NopPathRule());

        public static IPathRule Instance => LazyInstance.Value;

        private NopPathRule()
        {
            
        }

        /// <inheritdoc />
        public void Apply(IMarkingsProvider markings, Path path) { }
    }
}