using Microsoft.AspNetCore.Mvc;
using RPDSerice.Models;
using RPDSerice.RPDGenerator.Interfaces;
using RPDSerice.RpdRepository.Implementation;
namespace RPDService.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class RPDController : ControllerBase
{
	private readonly IRPDGenerator _RPDGenerator;
	private readonly RpdRepository _RpdRepository;
	public RPDController(IRPDGenerator RPDGenerator, RpdRepository RpdRepository)
	{
		_RpdRepository = RpdRepository;
		_RPDGenerator = RPDGenerator;
	}
	[HttpGet]
	public IActionResult GenerateRPD(string dto)
	{
		_RPDGenerator.GetRPDPdfBytes(dto);
		return Ok("Hello World");
	}
	[HttpGet]
	public IActionResult GetCriticalInfos()
	{
		return Ok(_RpdRepository.GetAllCriticalInfo());
	}
	
	[HttpPost]
	public IActionResult SearchCriticalInfos(CriticalInfo info)
	{
		Console.WriteLine("hello");
		return Ok(_RpdRepository.SearchCriticalInfo(info));
	}
}
