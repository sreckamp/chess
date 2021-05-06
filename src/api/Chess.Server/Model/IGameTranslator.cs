using Chess.Model.Stores;

namespace Chess.Server.Model
{
    public interface IGameTranslator
    {
        GameState FromModel(int id, GameStore store, bool includeMoves = false);

        (int, GameStore) ToModel(GameState state);
    }
}