using Farm.DTOS;

namespace Farm.Service.IService
{
	public interface IOrderService
	{
		public void PlaceOrder(OrderRequestDTO request);
	}
}
