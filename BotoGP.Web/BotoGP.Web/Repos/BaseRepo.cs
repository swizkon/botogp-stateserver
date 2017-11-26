/*
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using BotoGP.Web;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;

namespace BotoGP.stateserver.Repos
{
    public abstract class BaseRepo<T>
	{
		private const string DatabaseName = "BotoGP";

        private DocumentClient client;

        string CollectionName => typeof(T).Name + "Collection";

        public BaseRepo()
        {
            var all = Startup.Configuration;
            var endPointUri = Startup.Configuration.GetSection("AppSettings")["StorageEndpointUri"];
            var primaryKey = Startup.Configuration.GetSection("AppSettings")["StoragePrimaryKey"];

            this.client = new DocumentClient(new Uri(endPointUri), primaryKey);
        }

        public IEnumerable<T> All()
        {
			FeedOptions queryOptions = new FeedOptions { MaxItemCount = -1 };
            IQueryable<T> q = this.client.CreateDocumentQuery<T>(
					UriFactory.CreateDocumentCollectionUri(DatabaseName, CollectionName), queryOptions);
            
            return q.ToList<T>();
        }

		protected async Task CreateDocumentIfNotExists(string documentId, object document)
		{
			try
			{
				await this.client.ReadDocumentAsync(UriFactory.CreateDocumentUri(DatabaseName, CollectionName, documentId));
			}
			catch (DocumentClientException de)
			{
				if (de.StatusCode == HttpStatusCode.NotFound)
				{
					await this.client.CreateDocumentAsync(UriFactory.CreateDocumentCollectionUri(DatabaseName, CollectionName), document);
				}
				else
				{
					throw;
				}
			}
		}
    }
}

*/