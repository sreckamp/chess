using System.Collections.Generic;

namespace Chess.Server.Models
{
    public class BoardState
    {
        public int GameId { get; set; }
        public string Name { get; set; }
        public int Corners { get; set; }
        public int Width { get; set; }
        public string Corner { get; set; }
        public string Other { get; set; }
        public Dictionary<string, string> RotationMap { get; set; }
        public Piece[] Pieces { get; set; }
    }
}