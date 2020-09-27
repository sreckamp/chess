﻿using System.Collections.Generic;
using System.Linq;
using Chess.Model.Models;
using Chess.Model.Models.Board;

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

        public bool Applies(PieceType type) => m_pathSources.Any(source => source.Applies(type));

        public void Apply(Square square, ISquareProvider squares)
        {
            foreach (var path in m_pathSources
                .Where(source => source.Applies(square.Piece.Type))
                .SelectMany(source => source.GetPaths(square, squares).Where(path => path.AllowTake)))
            {
                m_pathRule.Apply(square, path, squares);
            }
        }
    }
}