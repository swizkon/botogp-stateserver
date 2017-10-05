using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace BotoGP.Hubs
{
    public class RaceHub : Hub
    {
        public Task Send(string message)
        {
            return Clients.All.InvokeAsync("Send", message);
        }

        public Task Move(string coords)
        {
            return Clients.All.InvokeAsync("Move", coords);
        }

        public Task CrashInfo(string racer)
        {
            return Clients.All.InvokeAsync("Crash", racer);
        }
    }
}