using Chess.Model.Models;

namespace Chess.Model.Actions
{
    public sealed class UpdateMarkersAction: IAction
    {
        public Color ActivePlayer { get; set; }
    }
}