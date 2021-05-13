using System.Drawing;
using System.Linq;
using Chess.Model.Actions;
using Chess.Model.Evaluation.Models;
using Chess.Model.Models;
using Chess.Model.Reducers;
using Chess.Model.Stores;
using Color = Chess.Model.Models.Color;

namespace Chess.Model
{
    public class Evaluator
    {
        private static readonly GameStore InitialStore = new GameStore
        {
            Board = BoardFactory.Instance.Create(Version.None),
            Markings = new MarkingStore(),
            Version = Version.None,
            CurrentColor = Color.None
        };

        public static readonly Evaluator Instance = new Evaluator();
        private readonly GameReducer m_reducer = new GameReducer();

        private Evaluator() { }

        public GameStore Init(Version version, GameBoard board = null)
        {
            var action = new InitializeAction {Version = version, Board = board};

            return Evaluate(m_reducer.Apply(action, Evaluator.InitialStore));
        }

        public GameStore Move(GameStore store, Point from, Point to)
        {
            var move = store.Markings.GetMarkers<MoveMarker>(from).FirstOrDefault(marker => marker.Move.To == to);

            return move == null
                ? store
                : NextPlayer(m_reducer.Apply(new MoveAction {Move = move.Move}, store));
        }

        private GameStore NextPlayer(GameStore store)
        {
            var next = Evaluate(store);

            if (!next.Markings.AvailableColors.Contains(next.CurrentColor))
            {
                next = NextPlayer(m_reducer.Apply(new NextPlayerAction{CurrentColor = next.CurrentColor}, next));
            }

            return next;
        }

        public GameStore Evaluate(GameStore store)
        {
            var action = new EvaluateBoardAction {CurrentColor = store.CurrentColor};
            return m_reducer.Apply(action, store);
        }
    }
}