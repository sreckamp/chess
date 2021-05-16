using System.Collections.Generic;
using Chess.Model.Models;
using Chess.Model.Stores;
using Chess.Server.Services.Model;

namespace Chess.Server.Services
{
    public interface IGameProviderService
    {
        GameStore GetGame(int id);
        IEnumerable<Game> ListGames();
        int CreateGame(Version version, string name);
        void Update(int id, GameStore store);
    }
}