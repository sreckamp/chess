using System.Drawing;

namespace Chess.Model.Actions
{
    public class SelectAction : IAction
    {
        public Point Selection { get; set; }
    }
}