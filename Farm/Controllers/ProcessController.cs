using Farm.DTOS;
using Farm.Service.IService;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class ProcessController : ControllerBase
{
	private readonly IProcessService _processService;

	public ProcessController(IProcessService processService)
	{
		_processService = processService;
	}

	[HttpGet]
	public IActionResult GetAllProcess()
	{
		var processes = _processService.GetAllProcess();
		return Ok(processes);
	}

	[HttpGet("{id}")]
	public IActionResult GetProcessById(int id)
	{
		var process = _processService.GetProcessById(id);
		if (process == null)
		{
			return NotFound();
		}
		return Ok(process);
	}

	[HttpGet("GetAllProcessByProduceId/{id}")]
	public IActionResult GetProcessByProduceId(int id)
	{
		var process = _processService.GetAllProcessByProduceId(id);
		if (process == null)
		{
			return NotFound();
		}
		return Ok(process);
	}

	[HttpPost]
	public IActionResult AddProcess([FromBody] ProcessRequestDTO process)
	{
		_processService.AddProcess(process);
		return Ok("Process added successfully.");
	}

    [HttpPut("update/{id}")]
    public IActionResult UpdateProcess(int id, [FromBody] ProcessRequestDTO process)
	{
		_processService.UpdateProcess(id, process);
		return Ok("Process updated successfully.");
	}

	[HttpDelete("{id}")]
	public IActionResult DeleteProcess(int id)
	{
		_processService.DeleteProcess(id);
		return Ok("Process deleted successfully.");
	}

	[HttpGet("Search")]
	public IActionResult SearchProcess([FromQuery] string name)
	{
		var processes = _processService.SearchProcess(name);
		return Ok(processes);
	}
}
