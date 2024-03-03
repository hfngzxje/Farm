using System;
using System.Collections.Generic;

namespace Farm.Modelss
{
    public partial class Produce
    {
        public Produce()
        {
            Histories = new HashSet<History>();
            OrderDetails = new HashSet<OrderDetail>();
            Processes = new HashSet<Process>();
        }

        public int ProduceId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime? PlantingDate { get; set; }
        public DateTime? ExpectedHarvestDate { get; set; }
        public DateTime? ActualHarvestDate { get; set; }
        public int? GardenId { get; set; }
        public int? Quantity { get; set; }
        public int? Status { get; set; }
        public string? Img { get; set; }
        public double? Price { get; set; }

		public virtual Garden? Garden { get; set; }
        public virtual ICollection<History> Histories { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        public virtual ICollection<Process> Processes { get; set; }
    }
}
