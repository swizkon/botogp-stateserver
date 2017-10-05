using System;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace BotoGP.stateserver.Models
{
    public class CircuitMap
    {
        [JsonProperty(PropertyName = "checkPoints")]
		public List<CheckPoint> CheckPoints {get;set;} = new List<CheckPoint>();

        public List<CheckPoint> OnTrack { get; set; } = new List<CheckPoint>();

        public List<CheckPoint> OffTrack { get; set; } = new List<CheckPoint>();

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}