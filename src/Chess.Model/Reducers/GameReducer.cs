using Chess.Model.Actions;
using Chess.Model.Stores;

namespace Chess.Model.Reducers
{
    public class GameReducer : IReducer<GameStore>
    {
        private readonly BoardReducer m_boardReducer = new BoardReducer();
        private PlayerReducer m_playerReducer = new PlayerReducer();

        public GameStore Apply(IAction action, GameStore store)
        {
            try
            {
                switch (action)
                {
                    case InitializeAction ia:
                    {
                        m_playerReducer = new PlayerReducer(ia.Version);
                        return new GameStore
                        {
                            Version = ia.Version,
                            CurrentPlayer = m_playerReducer.Apply(action, store.CurrentPlayer),
                            Board = m_boardReducer.Apply(action, store.Board)
                        };
                    }
                    case MoveAction _:
                    {
                        //TODO: Verify current player
                        return new GameStore
                        {
                            Version = store.Version,
                            CurrentPlayer = m_playerReducer.Apply(action, store.CurrentPlayer),
                            Board = m_boardReducer.Apply(action, store.Board)
                        };
                        //TODO: Write this somewhere
                        //TODO: Write to move list
                    }
                    case SelectAction _:
                    {
                        //TODO: Verify current player
                        return new GameStore
                        {
                            Version = store.Version,
                            CurrentPlayer = m_playerReducer.Apply(action, store.CurrentPlayer),
                            Board = m_boardReducer.Apply(action, store.Board)
                        };
                    }
                }
            }
            // ReSharper disable once EmptyGeneralCatchClause
            catch { }

            return store;
        }
    }
}
