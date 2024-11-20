using System.Collections.Generic;
using System.Threading.Tasks;
using BotoGP.stateserver.Models;

namespace BotoGP.Domain.Services;

public interface ICircuitRepository
{
    Task<IEnumerable<Circuit>> ReadAll();

    Task<Circuit> Read(string id);

    Task Store(Circuit circuit);
}