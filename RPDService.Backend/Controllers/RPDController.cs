using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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
	[HttpPost]
	public IActionResult GenerateRPD(CriticalInfo dto)
	{
		_RPDGenerator.GetRPDPdfBytes(JsonConvert.SerializeObject(dto));
		return Ok("Hello World");
	}
	[HttpPost]
	public IActionResult CreateRPD(RPD rpd)
	{
		_RpdRepository.CreateRPD(rpd);
		return Ok("Hello World");
	}
	[HttpGet]
	public IActionResult GetCriticalInfos()
	{
		return Ok(_RpdRepository.GetAllCriticalInfo());
	}
	
	[HttpGet]
	public IActionResult GetAllRpd()
	{
		return Ok(_RpdRepository.GetAllRpd());
	}
	
	[HttpPost]
	public IActionResult SearchCriticalInfos(CriticalInfo info)
	{
		return Ok(_RpdRepository.SearchCriticalInfo(info));
	}
}
