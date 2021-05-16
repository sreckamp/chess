using Chess.Model.Stores;

namespace Chess.Server.Services.Model
{
    public class Game
    {
        public int Id { get; set; } = -1;
        public string Name { get; set; }
        public GameStore Store { get; set; }
    }
}