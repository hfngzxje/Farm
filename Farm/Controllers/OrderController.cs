using Microsoft.AspNetCore.Mvc;
using Farm.Service;
using Farm.DTOS;

[ApiController]
[Route("api/[controller]")]
public class OrderController : ControllerBase
{
	private readonly OrderService _orderService;

	public OrderController(OrderService orderService)
	{
		_orderService = orderService;
	}

	[HttpPost("PlaceOrder")]
	public IActionResult PlaceOrder([FromBody] OrderRequestDTO orderRequest)
	{
		try
		{
			_orderService.PlaceOrder(orderRequest);
			return Ok("Order placed successfully.");
		}
		catch (ArgumentException ex)
		{
			return BadRequest(ex.Message);
		}
		catch (Exception ex)
		{
			return StatusCode(500, $"Internal server error: {ex.Message}");
		}
	}
}
