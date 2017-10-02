
using System;
using Newtonsoft.Json;

namespace BotoGP.stateserver.Models
{
    public class CheckPoint
    {
        public int x { get; set; }

        public int y { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}