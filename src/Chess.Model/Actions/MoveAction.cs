using System.Drawing;

namespace Chess.Model.Actions
{
    public sealed class MoveAction : IAction
    {
        public Point From { get; set; }
        public Point To { get; set; }
    }
}