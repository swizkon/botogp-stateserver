﻿using System;
namespace BotoGP.stateserver.Models
{
	public class RaceState
	{
		public string RiderId { get; set; }
		public string RiderKey { get; set; }

		public int ForceX { get; set; }
		public int ForceY { get; set; }

		public int PosX { get; set; }
		public int PosY { get; set; }

		//public RaceState()
		//{
		//}
	}
}
