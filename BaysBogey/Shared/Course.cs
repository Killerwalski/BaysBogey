using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BaysBogey.Shared
{
    public class Course
    {
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
        public List<Hole> Holes { get; set; }
    }
}
