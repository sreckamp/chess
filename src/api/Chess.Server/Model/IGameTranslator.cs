using Chess.Model.Stores;
using Chess.Server.Model;

namespace Chess.Server.Services
{
    public interface IGameTranslator
    {
        GameState fromModel(int id, GameStore store);
        (int, GameStore) toModel(GameState state);
    }
}