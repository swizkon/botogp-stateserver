

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
    }

    public async Task<Circuit> Read(string id)
    {
        var result = await File.ReadAllTextAsync(Path.Combine(Environment.CurrentDirectory, "circuits", $"{id}.json"));

        return Newtonsoft.Json.JsonConvert.DeserializeObject(result) as Circuit;
    }

    public async Task Store(Circuit circuit)
    {
        await File.WriteAllTextAsync(Path.Combine(Environment.CurrentDirectory, "circuits", $"{circuit.Id}.json"), Newtonsoft.Json.JsonConvert.SerializeObject(circuit));
    }
}
