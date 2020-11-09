﻿using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Chess.Model.Models;
using Chess.Model.Models.Board;
using Chess.Model.Stores;

namespace Chess.Model.Rules
{
    public sealed class MarkSquareRules : IRules
    {
        private readonly IEnumerable<IPathSource> m_pathSources;
        private readonly IPathRule m_pathRule;

        public MarkSquareRules(IEnumerable<IPathSource> pathSources, IPathRule pathRule)
        {
            m_pathSources = pathSources;
            m_pathRule = pathRule;
        }

        /// <inheritdoc/>
        public void Apply(Point start, Piece piece, IPieceEnumerationProvider squares, MarkingStore store)
        {
            foreach (var path in m_pathSources
                .SelectMany(source => source.GetPaths(start, piece, squares).Where(path => path.AllowTake)))
            {
                m_pathRule.Apply(store, path);
            }
        }
    }
}