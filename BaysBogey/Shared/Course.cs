using System;
using System.Collections.Generic;
using System.Text;

namespace BaysBogey.Shared
{
    public class Course
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<Hole> Holes { get; set; }
    }
}
