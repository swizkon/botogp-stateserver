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
        string CollectionName = "Circuits";

        //public IEnumerable<Circuit> All()
        //{
        //    return base.All();
        //}

        public async Task CreateIfNotExists(Circuit circuit)
        {
            await base.CreateDocumentIfNotExists(circuit.Id.ToString(), circuit);
        }
    }
}
