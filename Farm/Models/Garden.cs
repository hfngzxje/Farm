using System;
using System.Collections.Generic;

namespace Farm.Modelss
{
    public partial class Garden
    {
        public Garden()
        {
            Produces = new HashSet<Produce>();
        }

        public int GardenId { get; set; }
        public string? Name { get; set; }
        public string? Location { get; set; }
        public decimal? Area { get; set; }

        public virtual ICollection<Produce> Produces { get; set; }
    }
}
