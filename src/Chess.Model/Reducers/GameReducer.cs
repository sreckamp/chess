using System.Collections.Generic;
using Chess.Model.Actions;
using Chess.Model.Models;
using Chess.Model.Stores;

namespace Chess.Model.Reducers
{
    public class GameReducer : IReducer<GameStore>
    {
        private readonly IReducer<Version> m_versionReducer;
        private readonly IReducer<GameBoard> m_boardReducer;
        // private readonly IReducer<State> m_stateReducer;
        private readonly IReducer<Color> m_playerReducer;
        private readonly IReducer<HistoryStore> m_historyReducer;

        public GameReducer(IReducer<Version> versionReducer, IReducer<GameBoard> boardReducer,
            // IReducer<SideStore> sideReducer,
            IReducer<Color> playerReducer,
            IReducer<HistoryStore> historyReducer)
        {
            m_versionReducer = versionReducer;
            m_boardReducer = boardReducer;
            // m_stateReducer = sideReducer;
            m_playerReducer = playerReducer;
            m_historyReducer = historyReducer;
        }

        public GameStore Apply(IAction action, GameStore store)
        {
            try
            {
                return new GameStore
                {
                    Version = m_versionReducer.Apply(action, store.Version),
                    CurrentPlayer = m_playerReducer.Apply(action, store.CurrentPlayer),
                    Board = m_boardReducer.Apply(action, store.Board),
                    // State = m_stateReducer.Apply(action, new SideStore
                    // {
                    //     Sides = store.Sides
                    // }).Sides,
                    HistoryItems = m_historyReducer.Apply(action, new HistoryStore
                    {
                        Board = store.Board,
                        History = store.HistoryItems
                    }).History
                };
            }
            // ReSharper disable once EmptyGeneralCatchClause
            catch
            {
            }

            return store;
        }
    }
}
