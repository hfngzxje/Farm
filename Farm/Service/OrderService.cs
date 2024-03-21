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

		public void DeleteOrder(int id)
		{
			var orderDetails = _context.OrderDetails.Where(od => od.OrderId == id);
			_context.OrderDetails.RemoveRange(orderDetails);

			var order = _context.Orders.Find(id);
			_context.Orders.Remove(order);

			_context.SaveChanges();
		}

		public List<Order> GetAllOrder()
		{
			return _context.Orders.ToList();
		}

		public List<Order> GetOrdersByUserId(int userId)
		{
			return _context.Orders.Where(o => o.UserId == userId).ToList();
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
					Status = 1
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
						TotalPrice = orderDetail.Quantity * orderDetail.Price,
						Address = orderDetail.Address
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

        public Order GetOrderById(int id)
        {
            var order = _context.Orders.Find(id);
            if (order == null)
            {
                throw new System.Exception("Order not found");
            }

            return order;
        }

        public List<OrderDetail> GetAllOrderDetialsByOrderId(int orderId)
        {
            try
            {
                var orderDetails = _context.OrderDetails.Where(p => p.OrderId == orderId).ToList();
                return orderDetails;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error: {ex.Message}");
            }
        }


        public void UpdateOrder(int id, OrderUpdateRequestDTO request)
        {
            var order = _context.Orders.FirstOrDefault(o => o.OrderId == id);

            if (order == null)
            {
                throw new ArgumentException($"Order with ID {id} not found.");
            }

            order.UserId = request.UserID;
            order.OrderDate = request.OrderDate;
            order.Status = request.Status;

            try
            {
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating order.", ex);
            }
        }
    }
}
