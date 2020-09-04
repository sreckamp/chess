using System.Collections.Generic;
using Chess.Model.Models;
using Color = Chess.Model.Models.Color;

namespace Chess.Model.Stores
{
    public class GameStore
    {
        public Version Version { get; set; }
        public Color CurrentPlayer { get; set; }
        public BoardStore Board { get; set; }
    }
}