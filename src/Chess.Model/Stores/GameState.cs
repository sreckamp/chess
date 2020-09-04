using System.Collections.Generic;
using Chess.Model.Models;

namespace Chess.Model
{
    public class GameStore
    {
        public GameStore()
        {
            MoveHistory = new List<Move>();
        }

        public List<Move> MoveHistory { get; }

        public BoardStore Board { get; private set; }
    }
}