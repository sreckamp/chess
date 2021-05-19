using System.Collections.Generic;
using Chess.Model.Models;

namespace Chess.Server.Services.Model
{
    public class GameFile
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CurrentColor { get; set; }
        public string Version { get; set; }
        public int Corners { get; set; }
        public int Size { get; set; }
        public IEnumerable<PlacementFile> Placements { get; set; }
    }

    public class PlacementFile
    {
        public int X { get; set; }
        public int Y { get; set; }
        public string Color { get; set; }
        public string Piece { get; set; }
        public bool HasMoved { get; set; }
    }
}
