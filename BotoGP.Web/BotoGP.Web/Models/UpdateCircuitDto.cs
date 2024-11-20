using System;
using System.Collections.Generic;

namespace BotoGP.stateserver.Models;

public class UpdateCircuitDto
{
    public string Name { get; set; }
        
    public List<CheckPoint> CheckPoints { get; set; }

    public List<CheckPoint> OnTrack { get; set; }

    public List<CheckPoint> OffTrack { get; set; }
}