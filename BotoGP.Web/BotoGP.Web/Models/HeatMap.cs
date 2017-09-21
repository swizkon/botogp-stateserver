using System;
using Newtonsoft.Json;

namespace BotoGP.stateserver.Models
{
    public class HeatMap
    {
        [JsonProperty(PropertyName = "id")]
        public Guid CircuitId { get; set; } = Guid.NewGuid();

		public string Points { get; set; }

		public override string ToString()
		{
			return JsonConvert.SerializeObject(this);
		}
    }
}