using System.Collections.Generic;
using System.Threading.Tasks;
using Chess.Model.Models;
using Chess.Model.Stores;
using Chess.Server.Services.Model;

namespace Chess.Server.Services
{
    public interface IGameProviderService
    {
        Task<Game> GetGame(int id);
        Task<IEnumerable<Game>> ListGames();
        Task<int> CreateGame(Version version, string name);
        Task<Game> UpdateStore(int id, GameStore store);
    }
}