﻿using System;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace BotoGP.stateserver.Models
{
    public class HeatMap
    {
        public IDictionary<string, int> PointsOfInterest { get; set; } = new Dictionary<string, int>();

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}