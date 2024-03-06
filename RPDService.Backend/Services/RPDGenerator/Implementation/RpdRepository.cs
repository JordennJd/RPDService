using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RPDSerice.Models;
using RPDSerice.RpdRepository.SearchEngine;
using RPDService.Dtos;
namespace RPDSerice.RpdRepository.Implementation;

public class RpdRepository
{
	private readonly ApplicationDbContext _context;
	private readonly RpdSearchEngine _rpdSearchEngine;

	public RpdRepository(ApplicationDbContext context, RpdSearchEngine rpdSearchEngine)
	{
		_context = context;
		_rpdSearchEngine = rpdSearchEngine;
	}
	
	public void CreateRPD(RPD rpd)
	{
		var CriticalInfos = _rpdSearchEngine.SearchCriticalInfoByCriticalInfo(rpd.CriticalInfo);
		if(CriticalInfos.Count()!= 1)
		{
			throw new ArgumentException("cant identify rpd critical template");
		}
		rpd.CriticalInfo.Id = CriticalInfos.ElementAt(0).Id;
		rpd.CriticalInfo = CriticalInfos.ElementAt(0);
		rpd.CreatorId = 0;
		_context.Add(rpd);
		_context.SaveChanges();
	}
	public void DeleteRPD(RPD rpd)
	{
		var findedRpd = _rpdSearchEngine.SearchRpdByRpd(rpd);
		if (findedRpd == null)
		{
			throw new ArgumentException("such rpd is not exist");
		}
		if(findedRpd.Count() != 1)
		{
			throw new ArgumentException("cant identify rpd");

		}
		_context.RPDs.Remove(findedRpd.First());
		_context.SaveChanges();
	}

	public IEnumerable<CriticalInfo> GetAllCriticalInfo()
	{
		return _context.CriticalInfo.ToList();
	}
	
	public IEnumerable<RPD> GetAllRpd()
	{
		return _context.RPDs
		.Include(r=>r.RpdInfo)
		.Include(r=>r.CriticalInfo)
		.ToList();
	}
	

	public void ChangeCriticalInfos(ChangeCriticalDto dto)
	{
		var list = _rpdSearchEngine.SearchRpdByCriticalInfo(dto.source);
		if(list.Count()!= 1)
		{
			throw new ArgumentException("cant identify rpd critical template");
		}
		_context.CriticalInfo.Remove(list.First().CriticalInfo);
		_context.CriticalInfo.Add(dto.des);
		_context.SaveChanges();
	}

	public void ChangeRpdInfoByCriticalInfo(ChangeRpdInfoDtoByCriticalInfo dto)
	{
		var list = _rpdSearchEngine.SearchCriticalInfoByCriticalInfo(dto.source);
		if (list.Count() != 1)
		{
			throw new ArgumentException("cant identify rpd critical template");
		}
		var temp = _context.RPDs.Include(r => r.RpdInfo).Where(r => r.CriticalInfo.Equals(list.First()));
		foreach (var r in temp)
		{
			r.RpdInfo = dto.des;
		}
		_context.SaveChanges();
	}

	public void ChangeRpdInfoByRpd(ChangeRpdInfoDtoByRpd dto)
	{
		var list = _rpdSearchEngine.SearchRpdByRpd(dto.source);
		var temp = _context.RPDs.Include(r => r.RpdInfo).Where(r=> list.Contains(r));
		foreach (var r in temp)
		{
			r.RpdInfo = dto.des;
		}
		_context.SaveChanges();
	}
}
