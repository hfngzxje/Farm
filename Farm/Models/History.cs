using System;
using System.Collections.Generic;

namespace Farm.Modelss
{
    public partial class History
    {
        public int HistoryId { get; set; }
        public string? Description { get; set; }
        public DateTime? Timestamp { get; set; }
        public int? UserId { get; set; }
        public int? ProduceId { get; set; }

        public virtual Produce? Produce { get; set; }
        public virtual User? User { get; set; }
    }
}
