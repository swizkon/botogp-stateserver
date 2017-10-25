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

        public Task NextMove(string racer, int move)
        {
            return RaceStateChange(racer, 0, 0, move, move);
            // return Clients.All.InvokeAsync("Move", racer, move, move);
        }

        public Task RaceStateChange(
            string racer, 
            int x, 
            int y, 
            int verticalVelocity, 
            int horizontalVelocity)
        {
            return Clients.All.InvokeAsync("RaceStateChange", racer, x, y, verticalVelocity, horizontalVelocity);
        }

        public Task Move(string racer, int x, int y)
        {
            return Clients.All.InvokeAsync("Move", racer, x, y);
        }

        public Task CrashInfo(string racer)
        {
            return Clients.All.InvokeAsync("Crash", racer);
        }

        public void JoinTour(string racer, string tour)
        {
            this.Groups.AddAsync(this.Context.ConnectionId, tour);

            this.RaceStateChange(racer, 0,0,0,0);
        }

        public Task LeaveRace(string raceId)
        {
            return Groups.RemoveAsync(Context.ConnectionId, raceId);
        }

    }
}