using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotoGP.stateserver.Models;
using BotoGP.stateserver.Repos;
using BotoGP.Web;
using BotoGP.Projections;
using Microsoft.AspNetCore.Mvc;

using System.Net.Http;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BotoGP.stateserver.Controllers
{
    [Route("api/[controller]")]
    public class CircuitsController : Controller
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
                    Id = new Guid("a2cb2c01-e7c3-4f23-8148-85d8e7eb726a"),
                    Name = "Le Mans",
                    Checkpoints = "[[75,20],[42,73],[86,62],[133,40]]",
                    Map = new CircuitMap()
                    {
                        CheckPoints = new List<CheckPoint>(new []{
                            new CheckPoint(75, 20),
                            new CheckPoint(42, 73),
                            new CheckPoint(86, 62),
                            new CheckPoint(133, 40)
                        })
                    }
                };

                var assen = new Circuit()
                {
                    Id = new Guid("f1c0ce30-23e6-41c2-a7c7-27a468382d73"),
                    Name = "Assen TT",
                    Checkpoints = "[[75,20],[12,16],[40,49],[93,52],[121,74],[122,36],[75,32]]",
                    Map = new CircuitMap()
                    {
                        CheckPoints = new List<CheckPoint>(new []{
                            new CheckPoint(75, 20),
                            new CheckPoint(12, 16),
                            new CheckPoint(40, 49),
                            new CheckPoint(93, 52),
                            new CheckPoint(121, 74),
                            new CheckPoint(122, 36),
                            new CheckPoint(75,32 )
                        })
                    }
                };

                cache = new List<Circuit>(new[] { leMans, assen });
            }
            return cache ?? (cache = new List<Circuit>(repo.All()));
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public Circuit Get(string id)
        {
            return Get().FirstOrDefault(x => x.Id.ToString() == id);
        }

        // GET api/values/5
        [HttpGet("references")]
        public IEnumerable<CircuitReference> GetReferences()
        {
            return Get().Select(x => new CircuitReference{
                    Id = x.Id.ToString(),
                    Name = x.Name
            });
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

        [HttpPost("{id}")]
        public async Task<Circuit> Post(string id, [FromBody]UpdateCircuitDto model)
        {
            var c = this.Get(id);
            
            if(!string.IsNullOrWhiteSpace(model?.Name) )
                c.Name = model.Name;
            
            if(model?.CheckPoints != null)
            {
                c.Checkpoints = "[" + string.Join(",", model
                                .CheckPoints
                                .Select(p => $"[{p.x},{p.y}]")) + "]";

                c.Map.CheckPoints = model.CheckPoints;
            }
            
            if(model?.OnTrack != null)
            {
                c.Map.OnTrack = model.OnTrack;
            }
            
            if(model?.OffTrack != null)
            {
                c.Map.OffTrack = model.OffTrack;
            }

            var client = new HttpClient();
            var req = new HttpRequestMessage(HttpMethod.Post, "https://5564qhte39.execute-api.eu-west-1.amazonaws.com/prod/circuits?key=" + c.Id.ToString());
            req.Content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(c), System.Text.Encoding.UTF8, "application/json");
            await client.SendAsync(req);

            return c;
        }
    }
}
