using Chess.Model.Models;

namespace Chess.Model.Actions
{
    public sealed class NextPlayerAction: IAction
    {
        public Color CurrentColor { get; set; }
    }
}