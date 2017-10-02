using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotoGP.stateserver.Models;
using BotoGP.stateserver.Repos;
using BotoGP.Web;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BotoGP.stateserver.Controllers
{
    [Route("api/[controller]")]
    public class HeatMapsController : Controller
    {
        private static ConcurrentDictionary<string,string> cache 
            = new ConcurrentDictionary<string,string>();
        
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
            if(x < 0 || y < 0)
                return "Miss";

            var heat = Heat(id);
            
            return findHeat(heat, x, y);
        }

        private string findHeat(HeatMap heat, int x, int y)
        {
            if(x < 0)
                return "Miss";

            var key = new CheckPoint(x: x, y: y);

            if(heat.PointsOfInterest.ContainsKey(key))
                return heat.PointsOfInterest[key] == 1 ?  "Hit" : "Miss";

            return findHeat(heat, x-1, y);
        }

        private HeatMap Heat(string id)
        {
            var circuit = new CircuitsController().Get(id);

            return circuit.DataMap.Heat;
        }
    }
}
