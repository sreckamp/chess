using System.Collections.Generic;

namespace Chess.Server.Model
{
    public class GameState
    {
        public bool OpenForPlayers { get; set; }
        public int GameId { get; set; }
        public string Name { get; set; }
        public string CurrentPlayer { get; set; }
        public int Corners { get; set; }
        public int Size { get; set; }
        public IEnumerable<Piece> Pieces { get; set; }
        public IEnumerable<Move> MoveHistory { get; set; }
    }
}