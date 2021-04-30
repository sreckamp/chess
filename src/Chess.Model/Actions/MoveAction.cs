using Chess.Model.Move;

namespace Chess.Model.Actions
{
    public sealed class MoveAction : IAction
    {
        public IMove Move { get; set; }
    }
}