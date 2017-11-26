using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotoGP.Domain.Services;
using BotoGP.stateserver.Models;
// using BotoGP.stateserver.Repos;
using BotoGP.Web;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BotoGP.stateserver.Controllers
{
    [Route("api/[controller]")]
    public class HeatMapsController : Controller
    {
        private static ConcurrentDictionary<string, string> cache
            = new ConcurrentDictionary<string, string>();


        private readonly ICircuitRepository _circuitRepository;

        public HeatMapsController(ICircuitRepository circuitRepository)
        {
            _circuitRepository = circuitRepository;
        }

        [HttpGet("{id}/tileinfo")]
        public string GetTileInfo(string id, [FromQuery]int x, [FromQuery]int y)
        {
            return tileInfo(id, x, y);
        }

        private string tileInfo(string id, int x, int y)
        {
            var key = $"{id}/{x}:{y}";

            return cache.GetOrAdd(key, loadTileInfo(id, x, y));
        }

        private string loadTileInfo(string id, int x, int y)
        {
            if (x < 0 || y < 0)
                return "Miss";

            var heat = Heat(id);

            return findHeat(heat, x, y);
        }

        private string findHeat(IDictionary<string, int> heat, int x, int y, int retries = 0)
        {
            if (x < 0)
                return "Miss Out of bound after " + retries;

            var key = buildKey(x,y);

            if (heat.ContainsKey(key))
                return heat[key] == 1 ? "Hit in " + retries : "Miss in " + retries;

            return findHeat(heat, x - 1, y, retries + 1);
        }

        private IDictionary<string, int> Heat(string id)
        {
            var circuit = new CircuitsController(_circuitRepository).Get(id);

            var heat = new Dictionary<string, int>();

            circuit.Map.OffTrack.ForEach(o =>
            {
                heat[buildKey(o.x,o.y)] = 0;
            });
            circuit.Map.OnTrack.ForEach(o =>
            {
                heat[buildKey(o.x,o.y)] = 1;
            });

            return heat;
        }

        private string buildKey(int x, int y)
        {
            return "x:" + x + ",y:" + y;
        }
    }
}
