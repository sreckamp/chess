using Chess.Model.Models;

namespace Chess.Server.Model
{
    public class GameSummary
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Version Players { get; set; }
    }
}