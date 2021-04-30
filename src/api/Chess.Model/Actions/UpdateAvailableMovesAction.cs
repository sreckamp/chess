using Chess.Model.Models;

namespace Chess.Model.Actions
{
    public sealed class UpdateAvailableMovesAction: IAction
    {
        public Color ActivePlayer { get; set; }
    }
}