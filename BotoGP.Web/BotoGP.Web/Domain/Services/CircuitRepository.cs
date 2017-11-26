

namespace BotoGP.Domain.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Threading.Tasks;
    using BotoGP.stateserver.Models;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json.Linq;

    public class CircuitRepository : ICircuitRepository
    {
        IConfiguration _configuration;

        ILogger _logger;

        public CircuitRepository(IConfiguration configuration, ILogger<CircuitRepository> logger)
        {
            _configuration = configuration;
            _logger = logger;
            
            _logger.LogInformation("StorageConnectionString");
            _logger.LogInformation(_configuration["StorageConnectionString"]);
            _logger.LogInformation( string.Join(",", _configuration.AsEnumerable().Select(x => x.Key)));
        }

        public async Task<IEnumerable<Circuit>> ReadAll()
        {
            var client = new HttpClient();
            var req = new HttpRequestMessage(HttpMethod.Get, buildUrl("/circuits"));
            var result = await client.SendAsync(req);

            var data = await result.Content.ReadAsStringAsync();

            var array = Newtonsoft.Json.JsonConvert.DeserializeObject(data) as JArray;

            return array.ToObject<List<Circuit>>();
            // return d.Select(t => t.<Circuit>());

            // return Newtonsoft.Json.JsonConvert.DeserializeObject(data) as IList<Circuit>;
        }

        public async Task<Circuit> Read(string id)
        {
            var client = new HttpClient();
            var req = new HttpRequestMessage(HttpMethod.Get, buildUrl("/circuit?id=" + id));
            var result = await client.SendAsync(req);

            var data = result.Content.ReadAsStringAsync();

            return Newtonsoft.Json.JsonConvert.DeserializeObject(data.Result) as Circuit;
        }

        public async Task Store(Circuit circuit)
        {
            var client = new HttpClient();
            var req = new HttpRequestMessage(HttpMethod.Post, buildUrl("/circuits?key=" + circuit.Id.ToString()));
            req.Content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(circuit), System.Text.Encoding.UTF8, "application/json");
            await client.SendAsync(req);
        }

        string buildUrl(string path)
        {
            return Helpers.ConfigReader.ReadAppSetting(_configuration, "CircuitsRepositoryBaseUrl") + path;
        }
    }
}
