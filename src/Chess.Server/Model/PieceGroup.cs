using System.Collections.Generic;

namespace Chess.Server.Model
{
    public class PieceGroup
    {
        public string Color { get; set; }
        public IEnumerable<string> PieceTypes { get; set; }
    }
}