using System;
using System.Collections.Generic;

namespace Farm.Modelss
{
    public partial class OrderDetail
    {
        public int OrderDetailId { get; set; }
        public int? OrderId { get; set; }
        public int? ProduceId { get; set; }
        public int? Quantity { get; set; }
        public double? TotalPrice { get; set; }


		public virtual Order? Order { get; set; }
        public virtual Produce? Produce { get; set; }
    }
}
