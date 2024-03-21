using Farm.DTOS;
using Farm.Modelss;

namespace Farm.Service.IService
{
	public interface IOrderService
	{
		public void PlaceOrder(OrderRequestDTO request);

		List<Order> GetAllOrder();
		public List<Order> GetOrdersByUserId(int userId);
        public void UpdateOrder(int id, OrderUpdateRequestDTO request);

        void DeleteOrder(int id);
        Order GetOrderById(int id);
        public List<OrderDetail> GetAllOrderDetialsByOrderId(int orderId);
    }
}
