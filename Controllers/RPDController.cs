using Microsoft.AspNetCore.Mvc;
using RPDSerice.Models;
using RPDSerice.RPDGenerator.Interfaces;
namespace RPDService.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class RPDController : ControllerBase
{
	private readonly IRPDGenerator _RPDGenerator;
	
	public RPDController(IRPDGenerator RPDGenerator)
	{
		_RPDGenerator = RPDGenerator;
	}
	[HttpGet]
	public IActionResult GenerateRPD(string dto)
	{
		_RPDGenerator.GetRPDPdfBytes(dto);
		return Ok("Hello World");
	}
}
