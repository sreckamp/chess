﻿using Chess.Model.Evaluation.Models;

namespace Chess.Model.Evaluation.Rules
{
    public interface IPathRule
    {
        /// <summary>
        /// Execute the rule against the given path.
        /// </summary>
        /// <param name="markings">The markings to apply the rule to.</param>
        /// <param name="path">The sequence of moves that are available</param>
        void Apply(IMarkingsProvider markings, Path path);
    }
}