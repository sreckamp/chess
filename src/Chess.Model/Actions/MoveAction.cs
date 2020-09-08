using System.Drawing;

namespace Chess.Model.Actions
{
    public class MoveAction : IAction
    {
        public Point From { get; set; }
        public Point To { get; set; }
    }
}