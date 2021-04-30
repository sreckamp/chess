using Chess.Model.Actions;

namespace Chess.Model.Reducers
{
    public interface IReducer<T>
    {
        T Apply(IAction action, T store);
    }
}