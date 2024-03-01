using System;
using System.Collections.Generic;

namespace Farm.Models
{
    public partial class Order
    {
        public int OrderId { get; set; }
        public int? ProduceId { get; set; }
        public int? Quantity { get; set; }
        public DateTime? RequestDate { get; set; }
        public string? Status { get; set; }

        public virtual Produce? Produce { get; set; }
    }
}
