using System;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace BotoGP.stateserver.Models
{
    public class Circuit
    {
        [JsonProperty(PropertyName = "id")]
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Name { get; set; } = "";

        public int Width { get; set; } = 150;

		public int Height { get; set; } = 100;

        public int Scale { get; set; } = 5;

        public string Checkpoints { get; set; }
        
        public CircuitMap Map { get; set; } = new CircuitMap();

		public override string ToString()
		{
			return JsonConvert.SerializeObject(this);
		}
    }
}
