using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RPDSerice.Models;
using RPDSerice.RPDGenerator.Interfaces;
using RPDSerice.RpdRepository.Implementation;
using RPDSerice.RpdRepository.SearchEngine;
using RPDService.Dtos;
namespace RPDService.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class RPDController : ControllerBase
{
	private readonly IRPDGenerator _RPDGenerator;
	private readonly RpdRepository _RpdRepository;
	private readonly RpdSearchEngine _rpdSearchEngine;
	public RPDController(IRPDGenerator RPDGenerator, RpdRepository RpdRepository, RpdSearchEngine rpdSearchEngine)
	{
		_rpdSearchEngine = rpdSearchEngine;
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
		return Ok(_RpdRepository.GetAllCriticalInfo().ToList());
	}
	
	[HttpGet]
	public IActionResult GetAllRpd()
	{
		return Ok(_RpdRepository.GetAllRpd().ToList());
	}
	[HttpPost]	
	public IActionResult SearchRpd(CriticalInfo dto)
	{
		return Ok(_rpdSearchEngine.SearchRpdByCriticalInfo(dto).ToList());
	}

	[HttpPost]
	public IActionResult SearchCriticalInfos(CriticalInfo info)
	{
		return Ok(_rpdSearchEngine.SearchCriticalInfoByCriticalInfo(info).ToList());
	}
	[HttpPost]
	public IActionResult ChangeCriticalInfos(ChangeCriticalDto dto)
	{
		_RpdRepository.ChangeCriticalInfos(dto);
		return Ok();
	}
	[HttpPost]
	public IActionResult ChangeRpdInfosByCriticalInfo(ChangeRpdInfoDtoByCriticalInfo dto)
	{
		_RpdRepository.ChangeRpdInfoByCriticalInfo(dto);
		return Ok();
	}
	[HttpPost]
	public IActionResult ChangeRpdInfosByRpd(ChangeRpdInfoDtoByRpd dto)
	{
		_RpdRepository.ChangeRpdInfoByRpd(dto);
		return Ok();
	}
	[HttpPost]
	public IActionResult DeleteRPD(RPD dto)
	{
		_RpdRepository.DeleteRPD(dto);
		return Ok();
	}
}
