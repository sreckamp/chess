using System.Collections.Generic;
using System.Threading.Tasks;
using Chess.Model.Models;
using Chess.Model.Stores;
using Chess.Server.Services.Model;

namespace Chess.Server.Services
{
    public class AwsElasticacheGameProviderService : IGameProviderService
    {
        public Task<GameStore> GetGame(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<Game>> ListGames()
        {
            throw new System.NotImplementedException();
        }

        public Task<int> CreateGame(Version version, string name)
        {
            throw new System.NotImplementedException();
        }
        
        public Task Update(int id, GameStore store)
        {
            throw new System.NotImplementedException();
        }
    }
}