using System;
using Chess.Model.Evaluation.Rules;
using Chess.Model.Evaluation.Sources;
using Version = Chess.Model.Models.Version;

namespace Chess.Model.Reducers
{
    public sealed class GameReducerFactory
    {
        private static readonly Lazy<GameReducerFactory> LazyInstance = new Lazy<GameReducerFactory>(() => new GameReducerFactory());
        public static GameReducerFactory Instance => LazyInstance.Value;

        private GameReducerFactory()
        {
        }
        
        public GameReducer Create(Version version) => new GameReducer(new VersionReducer(), new BoardReducer(), 
            new PlayerReducer(version),
            new MarkingsReducer(new [] {PathRules.MarkRules, PathRules.MoveRules}, PathSources.Sources)
            );
    }
}