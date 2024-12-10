

using System;
using System.IO;
using System.Net;

using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using BotoGP.stateserver.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;

namespace BotoGP.Domain.Services;
public class CircuitRepository : ICircuitRepository
{
    public CircuitRepository(IConfiguration configuration)
    {
        // _configuration = configuration;
    }

    public async Task<IEnumerable<Circuit>> ReadAll()
    {

        throw new NotImplementedException();
        //
        // var circuits = Directory.GetFiles(Path.Combine(Environment.CurrentDirectory, "circuits"));
        //
        //
        //
        // foreach (var circuit in circuits)
        // {
        //     var json = await File.ReadAllTextAsync(circuit);
        //     
        //     yield return Newtonsoft.Json.JsonConvert.DeserializeObject(json) as Circuit;
        // }
        //
        // var result = await client.GetStringAsync(buildUrl("/circuits"));
        //
        // var array = Newtonsoft.Json.JsonConvert.DeserializeObject(result) as JArray;
        //
        // return array.ToObject<List<Circuit>>();
    }

    public async Task<Circuit> Read(string id)
    {
        var result = await File.ReadAllTextAsync(Path.Combine(Environment.CurrentDirectory, "circuits", $"{id}.json"));
        // var result = await client.GetStringAsync(buildUrl("/circuit?id=" + id));

        return Newtonsoft.Json.JsonConvert.DeserializeObject(result) as Circuit;
    }

    public async Task Store(Circuit circuit)
    {
        await File.WriteAllTextAsync(Path.Combine(Environment.CurrentDirectory, "circuits", $"{circuit.Id}.json"), Newtonsoft.Json.JsonConvert.SerializeObject(circuit));
        // await client.SendAsync(req);
    }
    //
    // string buildUrl(string path)
    // {
    //     return Helpers.ConfigReader.ReadAppSetting(_configuration, "CircuitsRepositoryBaseUrl") + path;
    // }
}

//
// public class CircuitRepository : ICircuitRepository
// {
//     IConfiguration _configuration;
//
//     ILogger _logger;
//
//     // private static HttpClient client = new HttpClient();
//
//     public CircuitRepository(IConfiguration configuration, ILogger<CircuitRepository> logger)
//     {
//         _configuration = configuration;
//         _logger = logger;
//             
//         _logger.LogInformation("StorageConnectionString");
//         _logger.LogInformation(_configuration["StorageConnectionString"]);
//     }
//
//     public async Task<IEnumerable<Circuit>> ReadAll()
//     {
//         var result = await client.GetStringAsync(buildUrl("/circuits"));
//
//         var array = Newtonsoft.Json.JsonConvert.DeserializeObject(result) as JArray;
//
//         return array.ToObject<List<Circuit>>();
//     }
//
//     public async Task<Circuit> Read(string id)
//     {
//         var result = await client.GetStringAsync(buildUrl("/circuit?id=" + id));
//
//         return Newtonsoft.Json.JsonConvert.DeserializeObject(result) as Circuit;
//     }
//
//     public async Task Store(Circuit circuit)
//     {
//         var req = new HttpRequestMessage(HttpMethod.Post, buildUrl("/circuits?key=" + circuit.Id.ToString()));
//         req.Content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(circuit), System.Text.Encoding.UTF8, "application/json");
//         await client.SendAsync(req);
//     }
//
//     string buildUrl(string path)
//     {
//         return Helpers.ConfigReader.ReadAppSetting(_configuration, "CircuitsRepositoryBaseUrl") + path;
//     }
// }