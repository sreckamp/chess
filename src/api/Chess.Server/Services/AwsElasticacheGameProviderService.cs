using System.Collections.Generic;
using Chess.Model;
using Chess.Model.Models;

namespace Chess.Server.Services
{
    public class AwsElasticacheGameProviderService : IGameProviderService
    {
        public Game GetGame(int id)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<(int, Game)> ListGames()
        {
            throw new System.NotImplementedException();
        }

        public int CreateGame(Version version)
        {
            throw new System.NotImplementedException();
        }
    }
}