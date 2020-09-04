﻿using System.Collections.Generic;
using Chess.Model.Actions;
using Chess.Model.Models;

namespace Chess.Model.Reducers
{
    public class PlayerReducer : IReducer<Color>
    {
        private readonly Dictionary<Color, Color> m_nextMap;
        public PlayerReducer(Version version = Version.TwoPlayer)
        {
            m_nextMap = m_nextMapPerVersion[version];
        }
        
        public Color Apply(IAction action, Color store)
        {
            switch (action)
            {
                case InitializeAction _:
                    return Color.White;
                case MoveAction _:
                    return m_nextMap[store];
                default:
                    return store;
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