using System.Collections.Generic;
using System.Threading.Tasks;
using Chess.Model.Models;
using Chess.Model.Stores;
using Chess.Server.Services.Model;

namespace Chess.Server.Services
{
    public interface IGameProviderService
    {
        Task<GameStore> GetGame(int id);
        Task<IEnumerable<Game>> ListGames();
        Task<int> CreateGame(Version version, string name);
        Task Update(int id, GameStore store);
    }
}