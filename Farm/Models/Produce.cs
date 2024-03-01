using System;
using System.Collections.Generic;

namespace Farm.Models
{
    public partial class Produce
    {
        public Produce()
        {
            Histories = new HashSet<History>();
            Orders = new HashSet<Order>();
            Processes = new HashSet<Process>();
        }

        public int ProduceId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime? PlantingDate { get; set; }
        public DateTime? ExpectedHarvestDate { get; set; }
        public DateTime? ActualHarvestDate { get; set; }
        public int? GardenId { get; set; }

        public virtual Garden? Garden { get; set; }
        public virtual ICollection<History> Histories { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Process> Processes { get; set; }
    }
}
