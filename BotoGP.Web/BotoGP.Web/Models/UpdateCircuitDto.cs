using System;
using System.Collections.Generic;

namespace BotoGP.stateserver.Models
{
    public class UpdateCircuitDto
    {
        public string Name { get; set; }
        
        public List<CheckPoint> CheckPoints { get; set; }
        
        // public CircuitMap DataMap { get; set; } = new CircuitMap();
    }
}
