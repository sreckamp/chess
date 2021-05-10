using System.Collections.Generic;
using Chess.Model.Models;
using Chess.Model.Stores;

namespace Chess.Server.Services
{
    public interface IGameProviderService
    {
        GameStore GetGame(int id);
        IEnumerable<(int, GameStore)> ListGames();
        int CreateGame(Version version);
        void Update(int id, GameStore store);
    }
}