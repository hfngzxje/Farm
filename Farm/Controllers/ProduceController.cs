using Farm.DTOS;
using Farm.Modelss;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

[Route("api/[controller]")]
[ApiController]
public class ProduceController : ControllerBase
{
    private readonly IProduceService _produceService;

    public ProduceController(IProduceService produceService)
    {
        _produceService = produceService;
    }

    [HttpGet]
    public IActionResult GetAllProduces()
    {
        var produces = _produceService.GetAllProduces();
        return Ok(produces);
    }

    [HttpGet("getBy/{id}")]
    public IActionResult GetProduceById(int id)
    {
        try
        {
            var produce = _produceService.GetProduceById(id);
            return Ok(produce);
        }
        catch (Exception ex)
        {
            return BadRequest($"Error: {ex.Message}");
        }
    }

    [HttpPost("addProduce")]
    public IActionResult AddProduce([FromBody] ProduceRequestDTO produceRequest)
    {
        try
        {
            _produceService.AddProduce(produceRequest);
            return Ok("Produce added successfully!");
        }
        catch (System.Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("update/{id}")]
    public IActionResult UpdateProduce(int id, [FromBody] ProduceRequestDTO produceRequest)
    {
        try
        {
            _produceService.UpdateProduce(id, produceRequest);
            return Ok("Produce updated successfully");
        }
        catch (Exception ex)
        {
            return BadRequest($"Error: {ex.Message}");
        }
    }

	[HttpDelete("delete/{id}")]
	public IActionResult DeleteProduce(int id)
	{
		try
		{
			_produceService.DeleteProduce(id);
			return Ok("Produce deleted successfully");
		}
		catch (Exception ex)
		{
			return BadRequest($"Error: {ex.Message}");
		}
	}

	[HttpGet("Search")]
    public IActionResult SearchProduces(string? name)
    {
        try
        {
            var produces = _produceService.SearchProduces(name);
            return Ok(produces);
        }
        catch (Exception ex)
        {
            return BadRequest($"Error: {ex.Message}");
        }
    }

    [HttpGet("getAllByStatus")]
    public IActionResult getAllByStatus()
    {
        try
        {
            var produce = _produceService.GetAllByStatus();
            return Ok(produce);
        }
        catch (Exception ex)
        {
            return BadRequest($"Error: {ex.Message}");
        }
    }
}
