using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace Numerology.API.Hubs
{
    public class NotificationHub : Hub
    {
        public async Task SendMassage(string user, string message)
        {
            await Clients.All.SendCoreAsync("Basenotification", new object[] { user, message });
        }
    }
}
