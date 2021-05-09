using Chess.Model.Models;

namespace Chess.Model.Stores
{
    public class GameStore
    {
        public Version Version { get; set; }
        public Color CurrentColor { get; set; }
        public GameBoard Board { get; set; }
        
        public MarkingStore Markings { get; set; }
        
        // public MovesStore Moves { get; set; }
    }
}
