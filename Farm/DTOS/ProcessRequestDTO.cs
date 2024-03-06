namespace Farm.DTOS
{
	public class ProcessRequestDTO
	{
		public string? Name { get; set; }
		public string? Description { get; set; }
		public DateTime? StartTime { get; set; }
		public DateTime? EndTime { get; set; }
		public int? ProduceId { get; set; }
		public int? Status { get; set; }
	}
}
