﻿using System.Collections.Generic;
using Chess.Model.Actions;
using Chess.Model.Models;

namespace Chess.Model.Reducers
{
    public sealed class ColorReducer : IReducer<(Version, Color)>
    {
        public (Version, Color) Apply(IAction action, (Version, Color) store)
        {
            var (version, color) = store;
            return action switch
            {
                InitializeAction _ => (version, Color.White),
                MoveAction _ => (version, m_nextMapPerVersion[version][color]),
                _ => (version, color)
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