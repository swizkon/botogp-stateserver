using System;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace BotoGP.stateserver.Models;

public class HeatMap
{
    public IDictionary<CheckPoint, int> PointsOfInterest { get; set; } = new Dictionary<CheckPoint, int>();

    public override string ToString()
    {
        return JsonConvert.SerializeObject(this);
    }
}