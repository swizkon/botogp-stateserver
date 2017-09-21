using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotoGP.stateserver.Models;
using BotoGP.stateserver.Repos;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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
            return cache ?? (cache = new List<Circuit>(repo.All()));
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public Circuit Get(int id)
        {
            return new Circuit();
        }

        // POST api/values
        [HttpPost]
        public async void Post([FromQuery]string name)
        {
            var c = new Circuit
            {
                Name = name
            };
            await repo.CreateIfNotExists(c);

            cache.Add(c);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(string id, [FromBody]Circuit value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
