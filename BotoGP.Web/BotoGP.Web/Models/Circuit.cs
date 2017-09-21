using System;
using Newtonsoft.Json;

namespace BotoGP.stateserver.Models
{
    public class Circuit
    {
        [JsonProperty(PropertyName = "id")]
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Name { get; set; } = "Untitled";

        public int Width { get; set; } = 150;

		public int Height { get; set; } = 100;

        public int Scale { get; set; } = 5;

        public string Checkpoints { get; set; } = "[[150,20],[130,20],[30,50],[50,80],[20,180]]";

		// public string HeatMap { get; set; }

		public override string ToString()
		{
			return JsonConvert.SerializeObject(this);
		}
    }
}
