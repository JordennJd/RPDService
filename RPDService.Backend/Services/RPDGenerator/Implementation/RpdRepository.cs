using Microsoft.EntityFrameworkCore;
using RPDSerice.Models;
namespace RPDSerice.RpdRepository.Implementation;

public class RpdRepository
{
	private readonly ApplicationDbContext _context;

	public RpdRepository(ApplicationDbContext context)
	{
		_context = context;
	}

	public IEnumerable<CriticalInfo> GetAllCriticalInfo()
	{
		return _context.CriticalInfo.ToList();
	}
	
	public IEnumerable<CriticalInfo> SearchCriticalInfo(CriticalInfo info)
	{
		_context.CriticalInfo.Where(i =>
		(i.Faculty == info.Faculty || info.Faculty == "None") &&
		(i.SpecialtyNumber == info.SpecialtyNumber || info.SpecialtyNumber == "None") &&
		(i.SPZ == info.SPZ || info.SPZ == "None") &&
		(i.FO == info.FO || info.FO == "None") &&
		(i.GroupName == info.GroupName || info.GroupName == "None") &&
		(i.Name == info.Name || info.Name == "None") &&
		(i.NumberOfDepartament == info.NumberOfDepartament || info.NumberOfDepartament == "None") &&
		(i.TypeOfCourseProject == info.TypeOfCourseProject || info.TypeOfCourseProject == "None") &&
		(i.CountOfHourLecture == info.CountOfHourLecture || info.CountOfHourLecture == "None") &&
		(i.CountOfHourPractice == info.CountOfHourPractice || info.CountOfHourPractice == "None") &&
		(i.CountOfHourLab == info.CountOfHourLab || info.CountOfHourLab == "None") &&
		(i.CountOfHourCourseProject == info.CountOfHourCourseProject || info.CountOfHourCourseProject == "None") &&
		(i.CountOfHourCourseWork == info.CountOfHourCourseWork || info.CountOfHourCourseWork == "None") &&
		(i.ExamHours == info.ExamHours || info.ExamHours == "None") &&
		(i.SRS == info.SRS || info.SRS == "None") &&
		(i.CountOfHourCourseWork == info.CountOfHourCourseWork || info.CountOfHourCourseWork == "None") &&
		(i.TypeOfControl == info.TypeOfControl || info.TypeOfControl == "None")
		);
		
		return _context.CriticalInfo.ToList();
	}
}
