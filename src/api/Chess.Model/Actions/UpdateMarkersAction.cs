using Chess.Model.Models;

namespace Chess.Model.Actions
{
    public sealed class EvaluateBoardAction: IAction
    {
        public Color ActivePlayer { get; set; }
    }
}