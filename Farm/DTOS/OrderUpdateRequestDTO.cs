namespace Farm.DTOS
{
    public class OrderUpdateRequestDTO
    {
        public int UserID { get; set; }
        public DateTime OrderDate { get; set; }
        public int? Status { get; set; }
    }
}
