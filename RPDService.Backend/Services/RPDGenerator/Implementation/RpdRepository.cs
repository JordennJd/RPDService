using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RPDSerice.Models;
using RPDService.Dtos;
namespace RPDSerice.RpdRepository.Implementation;

public class RpdRepository
{
	private readonly ApplicationDbContext _context;

	public RpdRepository(ApplicationDbContext context)
	{
		_context = context;
	}
	
	public void CreateRPD(RPD rpd)
	{
		var CriticalInfos = SearchCriticalInfoByCriticalInfo(rpd.CriticalInfo);
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
	public IEnumerable<RPD> SearchRpd(CriticalInfo info)
	{
		return _context.RPDs
		.Include(r => r.RpdInfo)
		.Include(r => r.CriticalInfo)
		.ToList()
		.Where(i =>
		(i.CriticalInfo.Faculty == info.Faculty || info.Faculty == "None" || info.Faculty.IsNullOrEmpty()) &&
		(i.CriticalInfo.SpecialtyNumber == info.SpecialtyNumber || info.SpecialtyNumber == "None" || info.SpecialtyNumber.IsNullOrEmpty()) &&
		(i.CriticalInfo.FO == info.FO || info.FO == "None" || info.FO.IsNullOrEmpty()) &&
		(i.CriticalInfo.GroupName == info.GroupName || info.GroupName == "None" || info.GroupName.IsNullOrEmpty()) &&
		(i.CriticalInfo.Name == info.Name || info.Name == "None" || info.Name.IsNullOrEmpty())&&
		(i.CriticalInfo.TypeOfControl == info.TypeOfControl || info.TypeOfControl == "None" || info.TypeOfControl.IsNullOrEmpty()&&
		(i.CriticalInfo.NumberOfDepartament == info.NumberOfDepartament || info.NumberOfDepartament == "None" || info.NumberOfDepartament.IsNullOrEmpty()))
		);
	}
	public IEnumerable<RPD> SearchRpdByCriticalInfo(CriticalInfo info)
	{
		var list = _context.RPDs.Include(r => r.CriticalInfo).Where(i =>
		(i.CriticalInfo.Faculty == info.Faculty || info.Faculty == "None" || info.Faculty.IsNullOrEmpty()) &&
		(i.CriticalInfo.SpecialtyNumber == info.SpecialtyNumber || info.SpecialtyNumber == "None" || info.Faculty.IsNullOrEmpty()) &&
		(i.CriticalInfo.SPZ == info.SPZ || info.SPZ == "None" || info.SPZ.IsNullOrEmpty()) &&
		(i.CriticalInfo.FO == info.FO || info.FO == "None" || info.FO.IsNullOrEmpty()) &&
		(i.CriticalInfo.GroupName == info.GroupName || info.GroupName == "None" || info.GroupName.IsNullOrEmpty()) &&
		(i.CriticalInfo.Name == info.Name || info.Name == "None" || info.Name.IsNullOrEmpty()) &&
		(i.CriticalInfo.NumberOfDepartament == info.NumberOfDepartament || info.NumberOfDepartament == "None" || info.NumberOfDepartament.IsNullOrEmpty()) &&
		(i.CriticalInfo.TypeOfCourseProject == info.TypeOfCourseProject || info.TypeOfCourseProject == "None" || info.TypeOfCourseProject.IsNullOrEmpty()) &&
		(i.CriticalInfo.CountOfHourLecture == info.CountOfHourLecture || info.CountOfHourLecture == "None" || info.CountOfHourLecture.IsNullOrEmpty()) &&
		(i.CriticalInfo.CountOfHourPractice == info.CountOfHourPractice || info.CountOfHourPractice == "None" || info.CountOfHourPractice.IsNullOrEmpty()) &&
		(i.CriticalInfo.CountOfHourLab == info.CountOfHourLab || info.CountOfHourLab == "None" || info.CountOfHourLab.IsNullOrEmpty()) &&
		(i.CriticalInfo.CountOfHourCourseProject == info.CountOfHourCourseProject || info.CountOfHourCourseProject == "None" || info.CountOfHourCourseProject.IsNullOrEmpty()) &&
		(i.CriticalInfo.CountOfHourCourseWork == info.CountOfHourCourseWork || info.CountOfHourCourseWork == "None" || info.CountOfHourCourseWork.IsNullOrEmpty()) &&
		(i.CriticalInfo.ExamHours == info.ExamHours || info.ExamHours == "None" || info.Faculty.IsNullOrEmpty()) &&
		(i.CriticalInfo.SRS == info.SRS || info.SRS == "None" || info.SRS.IsNullOrEmpty()) &&
		(i.CriticalInfo.CountOfHourCourseWork == info.CountOfHourCourseWork || info.CountOfHourCourseWork == "None" || info.CountOfHourCourseWork.IsNullOrEmpty()) &&
		(i.CriticalInfo.TypeOfControl == info.TypeOfControl || info.TypeOfControl == "None" || info.TypeOfControl.IsNullOrEmpty())
		);
		
		return list.ToList();
	}

	public IEnumerable<CriticalInfo> SearchCriticalInfoByCriticalInfo(CriticalInfo info)
	{
		var list = _context.CriticalInfo.Where(i =>
		(i.Faculty == info.Faculty || info.Faculty == "None" || info.Faculty.IsNullOrEmpty()) &&
		(i.SpecialtyNumber == info.SpecialtyNumber || info.SpecialtyNumber == "None" || info.Faculty.IsNullOrEmpty()) &&
		(i.SPZ == info.SPZ || info.SPZ == "None" || info.SPZ.IsNullOrEmpty()) &&
		(i.FO == info.FO || info.FO == "None" || info.FO.IsNullOrEmpty()) &&
		(i.GroupName == info.GroupName || info.GroupName == "None" || info.GroupName.IsNullOrEmpty()) &&
		(i.Name == info.Name || info.Name == "None" || info.Name.IsNullOrEmpty()) &&
		(i.NumberOfDepartament == info.NumberOfDepartament || info.NumberOfDepartament == "None" || info.NumberOfDepartament.IsNullOrEmpty()) &&
		(i.TypeOfCourseProject == info.TypeOfCourseProject || info.TypeOfCourseProject == "None" || info.TypeOfCourseProject.IsNullOrEmpty()) &&
		(i.CountOfHourLecture == info.CountOfHourLecture || info.CountOfHourLecture == "None" || info.CountOfHourLecture.IsNullOrEmpty()) &&
		(i.CountOfHourPractice == info.CountOfHourPractice || info.CountOfHourPractice == "None" || info.CountOfHourPractice.IsNullOrEmpty()) &&
		(i.CountOfHourLab == info.CountOfHourLab || info.CountOfHourLab == "None" || info.CountOfHourLab.IsNullOrEmpty()) &&
		(i.CountOfHourCourseProject == info.CountOfHourCourseProject || info.CountOfHourCourseProject == "None" || info.CountOfHourCourseProject.IsNullOrEmpty()) &&
		(i.CountOfHourCourseWork == info.CountOfHourCourseWork || info.CountOfHourCourseWork == "None" || info.CountOfHourCourseWork.IsNullOrEmpty()) &&
		(i.ExamHours == info.ExamHours || info.ExamHours == "None" || info.Faculty.IsNullOrEmpty()) &&
		(i.SRS == info.SRS || info.SRS == "None" || info.SRS.IsNullOrEmpty()) &&
		(i.CountOfHourCourseWork == info.CountOfHourCourseWork || info.CountOfHourCourseWork == "None" || info.CountOfHourCourseWork.IsNullOrEmpty()) &&
		(i.TypeOfControl == info.TypeOfControl || info.TypeOfControl == "None" || info.TypeOfControl.IsNullOrEmpty())
		);

		return list.ToList();
	}

	public void ChangeCriticalInfos(ChangeCriticalDto dto)
	{
		var list = SearchRpdByCriticalInfo(dto.source);
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
		var list = SearchCriticalInfoByCriticalInfo(dto.source);
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
}
