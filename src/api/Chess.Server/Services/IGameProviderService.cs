using System.Collections.Generic;
using Chess.Model;
using Chess.Model.Models;

namespace Chess.Server.Services
{
    public interface IGameProviderService
    {
        Game GetGame(int id);
        IEnumerable<(int, Game)> ListGames();
        int CreateGame(Version version);
    }
}