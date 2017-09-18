using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using BotoGP.stateserver.Models;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;

namespace BotoGP.stateserver.Repos
{
    public class CircuitRepo : BaseRepo<Circuit>
    {
        const string CollectionName = "Circuits";

        public IEnumerable<Circuit> All()
        {
            return base.All(CollectionName);
        }

        public async Task CreateIfNotExists(Circuit circuit)
        {
            await base.CreateDocumentIfNotExists(CollectionName, circuit.Id.ToString(), circuit);
        }
    }
}
