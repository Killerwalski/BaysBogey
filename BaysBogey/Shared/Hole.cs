using AspNetMonsters.Blazor.Geolocation;
using System.Collections.Generic;

namespace BaysBogey.Shared
{
    public class Hole
    {
        public Dictionary<string, Location> TeeBoxes { get; set; }
        public Location Pin { get; set; }
    }
}