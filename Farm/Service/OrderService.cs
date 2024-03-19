using Farm.DTOS;
using Farm.Modelss;
using Farm.Service.IService;

namespace Farm.Service
{
	public class OrderService : IOrderService
	{
		private readonly FarmContext _context;

		public OrderService(FarmContext context)
		{
			_context = context;
		}

		public void PlaceOrder(OrderRequestDTO request)
		{
			if (request == null || request.OrderDetails == null || request.OrderDetails.Count == 0)
			{
				throw new ArgumentException("Invalid order data.");
			}

			try
			{
				foreach (var orderDetail in request.OrderDetails)
				{
					var produce = _context.Produces.Find(orderDetail.ProduceID);
					if (produce == null || produce.Quantity < orderDetail.Quantity)
					{
						throw new ArgumentException("Not enough stock for one or more products.");
					}
				}

				Order newOrder = new Order
				{
					UserId = request.UserID,
					OrderDate = DateTime.Now,
					Status = "Order Success"
				};

				_context.Orders.Add(newOrder);
				_context.SaveChanges();

				foreach (var orderDetail in request.OrderDetails)
				{
					OrderDetail newOrderDetail = new OrderDetail
					{
						OrderId = newOrder.OrderId,
						ProduceId = orderDetail.ProduceID,
						Quantity = orderDetail.Quantity,
						TotalPrice = orderDetail.Quantity * orderDetail.Price
					};

					_context.OrderDetails.Add(newOrderDetail);

					var produce = _context.Produces.Find(orderDetail.ProduceID);
					produce.Quantity -= orderDetail.Quantity;
					_context.Produces.Update(produce);
				}

				_context.SaveChanges();
			}
			catch (Exception ex)
			{
				throw new Exception($"Failed to place order: {ex.Message}");
			}
		}
	}
}
