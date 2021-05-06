using Chess.Model.Stores;
using Chess.Server.Model;

namespace Chess.Server.Services
{
    public interface IGameTranslator
    {
        GameState FromModel(int id, GameStore store);
        (int, GameStore) ToModel(GameState state);
    }
}