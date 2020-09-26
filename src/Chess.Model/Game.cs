using System.Drawing;
using Chess.Model.Actions;
using Chess.Model.Models;
using Chess.Model.Reducers;
using Chess.Model.Stores;

namespace Chess.Model
{
    public class Game
    {
        private GameReducer m_gameReducer;
        private readonly Version m_version;
        private readonly GameBoard m_board;

        public Game(Version version, GameBoard board = null)
        {
            m_version = version;
            Store = new GameStore();
            m_board = board ?? BoardStoreFactory.Instance.Create(m_version);
        }

        public GameStore Store { get; private set; }

        public void Init()
        {
            ApplyAndUpdate(new InitializeAction{Version = m_version, Board = m_board});
        }

        public void Move(Point from, Point to)
        {
            ApplyAndUpdate(new MoveAction {From = from, To = to});
        }

        private void ApplyAndUpdate(IAction action)
        {
            var next = Reducer.Apply(action, Store);
            Store = Reducer.Apply(new UpdateAvailableMovesAction(),
                Reducer.Apply(new UpdateMarkersAction{ActivePlayer = next.CurrentPlayer}, next));
        }

        private GameReducer Reducer => m_gameReducer ?? (m_gameReducer = GameReducerFactory.Instance.Create(m_version));
    }
}