using Microsoft.AspNetCore.SignalR;

namespace Chess.Server.Services
{
    public class GameEventHub: Hub<IGameUpdateClient>
    {
        
    }
}