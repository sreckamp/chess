using System.Drawing;

namespace Chess.Model.Actions
{
    public class MoveAction : IAction
    {
        public Point Target { get; set; }
    }
}