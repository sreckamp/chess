using System;
using Chess.Model.Actions;
using Chess.Model.Evaluation.Models;
using Chess.Model.Evaluation.Rules;
using Chess.Model.Evaluation.Sources;
using Chess.Model.Models;
using Chess.Model.Stores;
using Version = Chess.Model.Models.Version;

namespace Chess.Model.Reducers
{
    public sealed class GameReducer : IReducer<GameStore>
    {
        private readonly IReducer<GameBoard> m_boardReducer;
        private readonly IReducer<(Version, Color)> m_playerReducer;
        private readonly IReducer<(IPieceEnumerationProvider, MarkingStore)> m_markingReducer;

        public GameReducer(IReducer<GameBoard> boardReducer = null,
            IReducer<(Version, Color)> playerReducer = null,
            IReducer<(IPieceEnumerationProvider, MarkingStore)> markingReducer = null)
        {
            m_boardReducer = boardReducer ?? new BoardReducer();
            m_playerReducer = playerReducer ?? new ColorReducer();
            m_markingReducer = markingReducer
                               ?? new MarkingsReducer(new [] {PathRules.MarkRules, PathRules.MoveRules},
                                   PathSources.Sources);
        }

        public GameStore Apply(IAction action, GameStore store)
        {
            try
            {
                return new GameStore
                {
                    Version = action is InitializeAction ia ? ia.Version : store.Version,
                    CurrentColor = m_playerReducer.Apply(action, (store.Version, store.CurrentColor)).Item2,
                    Board = m_boardReducer.Apply(action, store.Board),
                    Markings = m_markingReducer.Apply(action, (store.Board, store.Markings)).Item2,
                };
            }
            catch(Exception e)
            {
                Console.Error.WriteLine("Error reducing state.", e);
            }

            return store;
        }
    }
}
