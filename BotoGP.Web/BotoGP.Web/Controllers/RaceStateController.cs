using BotoGP.stateserver.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Features;

using BotoGP.Hubs;

using System.Collections.Concurrent;

namespace BotoGP.stateserver.Controllers
{
    [Route("api/[controller]/{raceid}")]
    public class RaceStateController : Controller
    {
        private readonly IHubContext<RaceHub> _hubContext;

        private static ConcurrentDictionary<string, List<RaceState>> states
                    = new ConcurrentDictionary<string, List<RaceState>>();

        public RaceStateController(IHubContext<RaceHub> hubContext)
        {
            _hubContext = hubContext;
        }

        // GET api/racestate/race-123
        [HttpGet]
        public IEnumerable<RaceState> Get()
        {
            var race = states.GetValueOrDefault(RaceId());
            return race;
        }

        // GET api/racestate/race-123/rider-1
        [HttpGet("{id}")]
        public RaceState Get(string id)
        {
            var race = states.GetValueOrDefault(RaceId());
            return race?.FirstOrDefault(x => x.RiderId == id);
        }

        // POST api/values
        [HttpPost("{id}")]
        public void Post(string id,
                         [FromQuery]string key,
                         [FromQuery]int x,
                         [FromQuery]int y)
        {
            var racer = Get(id);
            if (racer != null && racer.RiderKey == key)
            {
                racer.ForceX += x;
                racer.ForceY += y;
                racer.PosX += racer.ForceX;
                racer.PosY += racer.ForceY;

                Update(racer.RiderId, racer.RiderKey, racer);
            }
			
			_hubContext.Clients.All.InvokeAsync("move", "racer-move", x, y);
        }

        // PUT api/racestate/race-123/rider-1
        [HttpPut("{id}")]
        public void Put(string id, [FromQuery]string key)
        {
            // Some code that resets the state 
            var state = new RaceState { RiderId = id, RiderKey = key };
            Update(id, key, state);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        private void Update(string id, string key, RaceState newState)
        {
            states.AddOrUpdate(RaceId()
                               , new List<RaceState>() { newState }
                            , (k, list) =>
                            {
                                list.RemoveAll(x => x.RiderId == id && x.RiderKey == key);
                                list.Add(newState);
                                return list;
                            });
        }

        private string RaceId()
        {
            return (string)this.RouteData.Values.GetValueOrDefault("raceid");
        }
    }
}
