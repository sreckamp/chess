﻿using System.Collections.Generic;
using Chess.Model.Actions;
using Chess.Model.Models;

namespace Chess.Model.Reducers
{
    public sealed class ColorReducer : IReducer<(Version, Color, IEnumerable<Color>)>
    {
        public (Version, Color, IEnumerable<Color>) Apply(IAction action, (Version, Color, IEnumerable<Color>) store)
        {
            var (version, color, available) = store;
            return action switch
            {
                InitializeAction _ => (version, Color.White, available),
                MoveAction _ => (version, m_nextMapPerVersion[version][color], available),
                _ => (version, color, available)
            };
        }

        private readonly Dictionary<Version, Dictionary<Color, Color>> m_nextMapPerVersion =
            new Dictionary<Version, Dictionary<Color, Color>>
            {
                {Version.TwoPlayer, new Dictionary<Color, Color>
                {
                    {Color.White, Color.Black},
                    {Color.Black, Color.White}
                }},
                {Version.FourPlayer, new Dictionary<Color, Color>
                {
                    {Color.White, Color.Silver},
                    {Color.Silver, Color.Black},
                    {Color.Black, Color.Gold},
                    {Color.Gold, Color.White}
                }}
            };
    }
}