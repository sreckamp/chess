using Chess.Model.Models;

namespace Chess.Model.Actions
{
    public sealed class InitializeAction : IAction
    {
        public Version Version { get; set; }
        public GameBoard Board { get; set; }
    }
}