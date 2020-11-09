using Chess.Model.Move;

namespace Chess.Model.Stores
{
    public class SideStore
    {
        public bool IsInCheck { get; set; }
        public IMove EnPassantMove { get; set; }


    }
}