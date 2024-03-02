using Farm.DTOS;
using Farm.Modelss;
using Farm.Service.IService;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

[Route("api/[controller]")]
[ApiController]
public class GardenController : ControllerBase
{
    private readonly IGardenService _gardenService;

    public GardenController(IGardenService gardenService)
    {
        _gardenService = gardenService;
    }

    [HttpGet]
    public IActionResult GetAllGardens()
    {
        var gardens = _gardenService.GetAllGardens();
        return Ok(gardens);
    }

    [HttpGet("getBy/{id}")]
    public IActionResult GetGardenById(int id)
    {
        try
        {
            var gardens = _gardenService.GetGardenById(id);
            return Ok(gardens);
        }
        catch (Exception ex)
        {
            return BadRequest($"Error: {ex.Message}");
        }
    }

    [HttpPost("addGarden")]
    public IActionResult AddGarden([FromBody] GardenRequestDTO gardenRequest)
    {
        try
        {
            _gardenService.AddGarden(gardenRequest);
            return Ok("Garden added successfully!");
        }
        catch (System.Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("update/{id}")]
    public IActionResult UpdateGarden(int id, [FromBody] GardenRequestDTO gardenRequest)
    {
        try
        {
            _gardenService.UpdateGarden(id, gardenRequest);
            return Ok("Garden updated successfully");
        }
        catch (Exception ex)
        {
            return BadRequest($"Error: {ex.Message}");
        }
    }

    [HttpDelete("delete/{id}")]
    public IActionResult DeleteGarden(int id)
    {
        try
        {
            _gardenService.DeleteGarden(id);
            return Ok("Garden deleted successfully");
        }
        catch (Exception ex)
        {
            return BadRequest($"Error: {ex.Message}");
        }
    }

    [HttpGet("Search")]
    public IActionResult SearchGardens(string? name)
    {
        try
        {
            var gardens = _gardenService.SearchGardens(name);
            return Ok(gardens);
        }
        catch (Exception ex)
        {
            return BadRequest($"Error: {ex.Message}");
        }
    }
}
