using Chess.Model.Models;

namespace Chess.Model.Actions
{
    public class UpdateMarkersAction: IAction
    {
        public Color ActivePlayer { get; set; }
    }
}