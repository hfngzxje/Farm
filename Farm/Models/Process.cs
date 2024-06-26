﻿using System;
using System.Collections.Generic;

namespace Farm.Modelss
{
    public partial class Process
    {
        public int ProcessId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public int? ProduceId { get; set; }
		public int? Status { get; set; }

		public virtual Produce? Produce { get; set; }
    }
}
