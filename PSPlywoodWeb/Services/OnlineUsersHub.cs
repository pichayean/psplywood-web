using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace PSPlywoodWeb.Services;

public class OnlineUsersHub : Hub
{
    public async Task UserConnected(string username)
    {
        await Clients.All.SendAsync("UserConnected", username);
    }

    public async Task UserDisconnected(string username)
    {
        await Clients.All.SendAsync("UserDisconnected", username);
    }
    
}
