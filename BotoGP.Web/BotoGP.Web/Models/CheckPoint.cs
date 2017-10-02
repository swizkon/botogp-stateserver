﻿
using System;
using Newtonsoft.Json;

namespace BotoGP.stateserver.Models
{
    public class CheckPoint : IEquatable<CheckPoint>
    {
        public CheckPoint()
        {
        }

        public CheckPoint(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public int x { get; set; }

        public int y { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }

        public bool Equals(CheckPoint other)
        {
            return this.y == other.y && this.x == other.x;
        }
    }
}