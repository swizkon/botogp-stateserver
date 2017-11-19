

namespace BotoGP.Domain.Services
{
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading.Tasks;
    using BotoGP.stateserver.Models;
    using Microsoft.Extensions.Configuration;

    public class CircuitRepository : ICircuitRepository
    {
        IConfiguration _configuration;

        public CircuitRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<IEnumerable<Circuit>> ReadAll()
        {
            var client = new HttpClient();
            var req = new HttpRequestMessage(HttpMethod.Get, buildUrl("/circuits"));
            var result = await client.SendAsync(req);

            var data = result.Content.ReadAsStringAsync();

            return Newtonsoft.Json.JsonConvert.DeserializeObject(data.Result) as IEnumerable<Circuit>;
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
            // "https://5564qhte39.execute-api.eu-west-1.amazonaws.com/prod/circuit?id=" + id;
        }
    }
}
