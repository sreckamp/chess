using Chess.Model.Actions;
using Chess.Model.Reducers;
using Chess.Model.Stores;

namespace Chess.Model
{
    public class Evaluator
    {
        public static readonly Evaluator Instance = new Evaluator();

        private Evaluator() { }

        public GameStore Evaluate(GameStore store)
        {
            var gameReducer = GameReducerFactory.Instance.Create(store.Version);
            var action = new EvaluateBoardAction {ActivePlayer = store.CurrentPlayer};
            return gameReducer.Apply(action, store);
        }
    }
}