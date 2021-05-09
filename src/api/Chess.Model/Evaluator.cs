using System.Drawing;
using System.Linq;
using Chess.Model.Actions;
using Chess.Model.Evaluation.Models;
using Chess.Model.Models;
using Chess.Model.Reducers;
using Chess.Model.Stores;

namespace Chess.Model
{
    public class Evaluator
    {
        public static readonly Evaluator Instance = new Evaluator();
        private readonly GameReducer m_reducer = new GameReducer();

        private Evaluator() { }

        public GameStore Init(Version version, GameBoard board = null)
        {
            var action = new InitializeAction {Version = version, Board = board};

            return Evaluate(m_reducer.Apply(action, null));
        }

        public GameStore Move(GameStore store, Point from, Point to)
        {
            var move = store.Markings.GetMarkers<MoveMarker>(from).FirstOrDefault(marker => marker.Move.To == to);

            return move == null
                ? store
                : Evaluate(m_reducer.Apply(new MoveAction {Move = move.Move}, store));
        }

        public GameStore Evaluate(GameStore store)
        {
            var action = new EvaluateBoardAction {ActivePlayer = store.CurrentColor};
            return m_reducer.Apply(action, store);
        }
    }
}