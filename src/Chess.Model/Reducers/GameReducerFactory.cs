﻿using System;
using Chess.Model.Rules;
using Version = Chess.Model.Models.Version;

namespace Chess.Model.Reducers
{
    public class GameReducerFactory
    {
        private static readonly Lazy<GameReducerFactory> LazyInstance = new Lazy<GameReducerFactory>(() => new GameReducerFactory());
        public static GameReducerFactory Instance => LazyInstance.Value;

        private GameReducerFactory()
        {
        }
        
        public GameReducer Create(Version version) => new GameReducer(new VersionReducer(), new BoardReducer(
            new MarkSquareRules(PathSources.Sources, PathRules.MarkRules), 
            new CollectAvailableRules(PathSources.Sources, PathRules.MoveRules)), 
            new PlayerReducer(version), new HistoryReducer());
    }
}