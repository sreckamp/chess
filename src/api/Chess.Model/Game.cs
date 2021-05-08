using System.Drawing;
using System.Linq;
using Chess.Model.Actions;
using Chess.Model.Evaluation.Models;
using Chess.Model.Models;
using Chess.Model.Reducers;
using Chess.Model.Stores;

namespace Chess.Model
{
    public class Game
    {
        private GameReducer m_gameReducer;
        public Version Version { get; }
        private readonly GameBoard m_board;

        public Game(Version version, GameBoard board = null)
        {
            Version = version;
            Store = new GameStore();
            m_board = board ?? BoardStoreFactory.Instance.Create(Version);
        }

        public GameStore Store { get; private set; }

        public void Init()
        {
            ApplyAndUpdate(new InitializeAction{Version = Version, Board = m_board});
        }

        public void Move(Point from, Point to)
        {
            var move = Store.Markings.GetMarkers<MoveMarker>(from).FirstOrDefault(marker => marker.Move.To == to);
            if (move == null) return;

            ApplyAndUpdate(new MoveAction {Move = move.Move});
        }

        private void ApplyAndUpdate(IAction action)
        {
            var next = Reducer.Apply(action, Store);
            Store = Reducer.Apply(new EvaluateBoardAction() {ActivePlayer = next.CurrentPlayer}, next);
        }

        private GameReducer Reducer => m_gameReducer ?? (m_gameReducer = GameReducerFactory.Instance.Create(Version));

        // /// <summary>
        // /// TODO: This should return the event for the particular player
        // /// </summary>
        // /// <param name="color"></param>
        // /// <returns></returns>
        // public async Task<object> GetEventsAsync(Color color)
        // {
        //     return new object();
        // }
        //
        // /// <summary>
        // /// This should trigger events for all the appropriate players
        // /// </summary>
        // private void PublishEvent()
        // {
        //     
        // }
    }
}