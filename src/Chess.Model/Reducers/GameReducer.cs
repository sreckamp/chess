using Chess.Model.Actions;
using Chess.Model.Evaluation.Models;
using Chess.Model.Models;
using Chess.Model.Models.Board;
using Chess.Model.Stores;

namespace Chess.Model.Reducers
{
    public sealed class GameReducer : IReducer<GameStore>
    {
        private readonly IReducer<Version> m_versionReducer;
        private readonly IReducer<GameBoard> m_boardReducer;
        // private readonly IReducer<State> m_stateReducer;
        private readonly IReducer<Color> m_playerReducer;
        private readonly IReducer<(IPieceEnumerationProvider, MarkingStore)> m_markingReducer;
        // private readonly IReducer<MovesStore> m_movesReducer;

        public GameReducer(IReducer<Version> versionReducer, IReducer<GameBoard> boardReducer,
            // IReducer<SideStore> sideReducer,
            IReducer<Color> playerReducer,
            IReducer<(IPieceEnumerationProvider, MarkingStore)> markingReducer//,
            // IReducer<MovesStore> movesReducer
            )
        {
            m_versionReducer = versionReducer;
            m_boardReducer = boardReducer;
            // m_stateReducer = sideReducer;
            m_playerReducer = playerReducer;
            m_markingReducer = markingReducer;
            // m_movesReducer = movesReducer;
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
                    Markings = m_markingReducer.Apply(action, (store.Board, store.Markings)).Item2,
                    // Moves = m_movesReducer.Apply(action, store.Moves)
                    // HistoryItems = m_historyReducer.Apply(action, new MovesStore
                    // {
                    //     Board = store.Board,
                    //     History = store.HistoryItems
                    // }).History
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
