﻿using System.Linq;
using Chess.Model.Models;
using Chess.Model.Models.Board;

namespace Chess.Model.Rules
{
    public sealed class PinPathRule : IPathRule
    {
        private readonly IPathRule m_chain;
        public PinPathRule(IPathRule chain)
        {
            m_chain = chain;
        }

        /// <inheritdoc />
        public void Apply(Square start, Path path, ISquareProvider squares)
        {
            if (path.Moves.Any())
            {
                var pin = start.GetMarkers(MarkerType.Pin).FirstOrDefault();

                if (pin != null)
                {
                    if (path.Direction != pin.Direction && path.Direction != pin.Direction.Opposite())
                    {
                        return;
                    }
                }
            }
            m_chain.Apply(start, path, squares);
        }
    }
}