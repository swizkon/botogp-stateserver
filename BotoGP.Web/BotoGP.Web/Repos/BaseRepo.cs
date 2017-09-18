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

        public BaseRepo()
		{
			var endPointUri = Startup.Configuration.GetSection("AppSettings")["StorageEndpointUri"];
			var primaryKey = Startup.Configuration.GetSection("AppSettings")["StoragePrimaryKey"];

            this.client = new DocumentClient(new Uri(endPointUri), primaryKey);
        }

        protected IEnumerable<T> All(string collectionName)
        {
			FeedOptions queryOptions = new FeedOptions { MaxItemCount = -1 };
            IQueryable<T> q = this.client.CreateDocumentQuery<T>(
					UriFactory.CreateDocumentCollectionUri(DatabaseName, collectionName), queryOptions);
            
            return q.ToList<T>();
        }


		protected async Task CreateDocumentIfNotExists(string collectionName, string documentId, object document)
		{
			try
			{
				await this.client.ReadDocumentAsync(UriFactory.CreateDocumentUri(DatabaseName, collectionName, documentId));
			}
			catch (DocumentClientException de)
			{
				if (de.StatusCode == HttpStatusCode.NotFound)
				{
					await this.client.CreateDocumentAsync(UriFactory.CreateDocumentCollectionUri(DatabaseName, collectionName), document);
				}
				else
				{
					throw;
				}
			}
		}
    }
}
