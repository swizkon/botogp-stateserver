using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotoGP.stateserver.Models;
using BotoGP.stateserver.Repos;
using BotoGP.Web;
using Microsoft.AspNetCore.Mvc;

namespace BotoGP.stateserver.Controllers
{
    [Route("api/[controller]")]
    public class CircuitController : Controller
    {
        private static List<Circuit> cache;
        CircuitRepo repo = new CircuitRepo();

        // GET: api/values
        [HttpGet]
        public IEnumerable<Circuit> Get()
        {
            if (cache == null && RuntimeEnvironment.IsDevelopment)
            {
                var leMans = new Circuit()
                {
                    Name = "Le Mans",
                    Checkpoints = "[[75,20],[66,23],[41,27],[36,63],[83,74],[102,69],[107,24],[83,31],[76,23]]"
                };

                var assen = new Circuit()
                {
                    Name = "Assen TT",
                    Checkpoints = "[[75,20],[74,21],[101,72],[56,52],[11,94],[44,88],[18,76],[25,45],[116,53],[130,89]]"
                };

                cache = new List<Circuit>(new[] { leMans, assen }
                );
            }
            // Le Mans
            // 

            // Assen TT
            // 


            return cache ?? (cache = new List<Circuit>(repo.All()));
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public Circuit Get(string id)
        {
            return Get().FirstOrDefault(x => x.Id.ToString() == id);
        }

        // POST api/values
        [HttpPost]
        public async void Post([FromQuery]string name)
        {
            var c = new Circuit
            {
                Name = name
            };

            if (!RuntimeEnvironment.IsDevelopment)
                await repo.CreateIfNotExists(c);

            cache.Add(c);
        }

        // PUT api/values/5
        [HttpPatch("{id}")]
        public Circuit Patch(string id, [FromBody]Circuit value)
        {
            var c = this.Get(id);
            
            if(!string.IsNullOrWhiteSpace(value.Name) )
                c.Name = value.Name;
            
            if(!string.IsNullOrWhiteSpace(value.Checkpoints) )
                c.Checkpoints = value.Checkpoints;
            
            if(value.DataMap != null)
                c.DataMap = value.DataMap;

            return c;
        }
    }
}
