using Microsoft.AspNetCore.Mvc;
using Farm.Service;
using Farm.DTOS;
using Farm.Modelss;
using Farm.Service.IService;

[ApiController]
[Route("api/[controller]")]
public class OrderController : ControllerBase
{
	private readonly OrderService _orderService;

	public OrderController(OrderService orderService)
	{
		_orderService = orderService;
	}


	[HttpGet]
	public IActionResult GetAllOrder()
	{
		var orders = _orderService.GetAllOrder();
		return Ok(orders);
	}

	[HttpGet("{userId}")]
	public ActionResult<List<Order>> GetOrderByUserId(int userId)
	{
		var orders = _orderService.GetOrdersByUserId(userId);
		if (orders == null)
		{
			return NotFound();
		}
		return orders;
	}

    [HttpPut("{id}")]
    public IActionResult UpdateOrder(int id, [FromBody] OrderUpdateRequestDTO request)
    {
        try
        {
            _orderService.UpdateOrder(id, request);
            return Ok("Order updated successfully");
        }
        catch (ArgumentException e)
        {
            return NotFound(e.Message);
        }
        catch (Exception e)
        {
            return StatusCode(500, $"Internal server error: {e.Message}");
        }
    }

    [HttpDelete("delete/{id}")]
	public IActionResult DeleteOrder(int id)
	{
		try
		{
			_orderService.DeleteOrder(id);
			return Ok("Order deleted successfully");
		}
		catch (Exception ex)
		{
			return BadRequest($"Error: {ex.Message}");
		}
	}

    [HttpGet("getBy/{id}")]
    public IActionResult GetOrderById(int id)
    {
        try
        {
            var order = _orderService.GetOrderById(id);
            return Ok(order);
        }
        catch (Exception ex)
        {
            return BadRequest($"Error: {ex.Message}");
        }
    }

    [HttpGet("GetAllOrderDetialsByOrderId/{id}")]
    public IActionResult GetAllOrderDetialsByOrderId(int id)
    {
        var orderDetails = _orderService.GetAllOrderDetialsByOrderId(id);
        if (orderDetails == null)
        {
            return NotFound();
        }
        return Ok(orderDetails);
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


    [HttpGet("{orderId}/export/pdf")]
    public async Task<IActionResult> ExportOrderToPdf(int orderId)
    {
        var pdfFile = await _orderService.ExportOrderToPdf(orderId);
        if (pdfFile == null)
            return NotFound();

        return File(pdfFile, "application/pdf", "order.pdf");
    }

    [HttpGet("{orderId}/export/word")]
    public async Task<IActionResult> ExportOrderToWord(int orderId)
    {
        var wordFile = await _orderService.ExportOrderToWord(orderId);
        if (wordFile == null)
            return NotFound();

        return File(wordFile, "application/msword", "order.docx");
    }

}
