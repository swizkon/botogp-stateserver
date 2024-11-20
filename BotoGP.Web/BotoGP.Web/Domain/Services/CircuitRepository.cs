

namespace BotoGP.Domain.Services;

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

    private static HttpClient client = new HttpClient();

    public CircuitRepository(IConfiguration configuration, ILogger<CircuitRepository> logger)
    {
        _configuration = configuration;
        _logger = logger;
            
        _logger.LogInformation("StorageConnectionString");
        _logger.LogInformation(_configuration["StorageConnectionString"]);
    }

    public async Task<IEnumerable<Circuit>> ReadAll()
    {
        var result = await client.GetStringAsync(buildUrl("/circuits"));

        var array = Newtonsoft.Json.JsonConvert.DeserializeObject(result) as JArray;

        return array.ToObject<List<Circuit>>();
    }

    public async Task<Circuit> Read(string id)
    {
        var result = await client.GetStringAsync(buildUrl("/circuit?id=" + id));

        return Newtonsoft.Json.JsonConvert.DeserializeObject(result) as Circuit;
    }

    public async Task Store(Circuit circuit)
    {
        var req = new HttpRequestMessage(HttpMethod.Post, buildUrl("/circuits?key=" + circuit.Id.ToString()));
        req.Content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(circuit), System.Text.Encoding.UTF8, "application/json");
        await client.SendAsync(req);
    }

    string buildUrl(string path)
    {
        return Helpers.ConfigReader.ReadAppSetting(_configuration, "CircuitsRepositoryBaseUrl") + path;
    }
}