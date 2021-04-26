﻿using System.Linq;
using Chess.Model.Models;
using Chess.Model.Models.Board;
 using Chess.Model.Stores;

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
        public void Apply(IMarkingsProvider markings, Path path)
        {
            if (path.Squares.Any())
            {
                var pin = markings.GetMarkers<SimpleMarker>(path.Start, MarkerType.Pin).FirstOrDefault();

                if (pin != default)
                {
                    if (path.Direction != pin.Direction && path.Direction != pin.Direction.Opposite())
                    {
                        return;
                    }
                }
            }
            m_chain.Apply(markings, path);
        }
    }
}