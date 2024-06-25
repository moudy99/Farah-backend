using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace Presentation.Hubs
{
    public class ChatHub: Hub
    {
    }


    public class CustomUserIdProvider : IUserIdProvider
    {
        public string GetUserId(HubConnectionContext connection)
        {
            return connection.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }
    }
}
