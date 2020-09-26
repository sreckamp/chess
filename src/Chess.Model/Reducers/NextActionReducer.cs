using Chess.Model.Actions;

namespace Chess.Model.Reducers
{
    public class NextActionReducer : IReducer<IAction>
    {
        public IAction Apply(IAction action, IAction store)
        {
            return store;
        }
    }
}