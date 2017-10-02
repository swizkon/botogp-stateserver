
using System;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace BotoGP.stateserver.Models
{
    public class CircuitMap
    {
        [JsonProperty(PropertyName = "heat")]
        public HeatMap Heat { get; set; } = new HeatMap();

        [JsonProperty(PropertyName = "checkPoints")]
		public List<CheckPoint> CheckPoints {get;set;} = new List<CheckPoint>();

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}