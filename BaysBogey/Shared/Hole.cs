using AspNetMonsters.Blazor.Geolocation;
using System.Collections.Generic;

namespace BaysBogey.Shared
{
    public class Hole
    {
        public int Number { get; set;}
        public int Par { get; set; }
        public Dictionary<string, Location> TeeBoxes { get; set; }
        public Location Pin { get; set; }
    }
}