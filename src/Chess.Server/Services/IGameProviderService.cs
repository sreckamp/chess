using Chess.Model;

namespace Chess.Server.Services
{
    public interface IGameProviderService
    {
        Game GetGame(int id);
        void StoreGame(int id, Game state);
    }
}