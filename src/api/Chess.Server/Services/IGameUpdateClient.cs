using System.Threading.Tasks;
using Chess.Server.Services.Model;

namespace Chess.Server.Services
{
    public interface IGameUpdateClient
    {
        public Task GameUpdated(GameUpdateMessage message);
    }
}