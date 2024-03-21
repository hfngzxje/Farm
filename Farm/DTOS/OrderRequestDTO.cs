namespace Farm.DTOS
{
	public class OrderRequestDTO
	{
		public int UserID { get; set; }
		public int? Status { get; set; }
		public List<OrderDetailRequest> OrderDetails { get; set; } 
	}

	public class OrderDetailRequest
	{
		public int ProduceID { get; set; }
		public int Quantity { get; set; } 
		public double Price { get; set; }
		public string? Address { get; set; }
	}
}
