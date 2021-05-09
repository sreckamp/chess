﻿using System.Collections.Generic;
using Chess.Model.Actions;
using Chess.Model.Models;

namespace Chess.Model.Reducers
{
    public sealed class PlayerReducer : IReducer<(Version, Color)>
    {
        public (Version, Color) Apply(IAction action, (Version, Color) store)
        {
            var (version, color) = store;
            switch (action)
            {
                case InitializeAction _:
                    return (version, Color.White);
                case MoveAction _:
                    return (version, m_nextMapPerVersion[version][color]);
                default:
                    return (version, color);
            }
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