using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
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
		var list = _context.CriticalInfo.Where(i =>
		(i.Faculty == info.Faculty || info.Faculty == "None" || info.Faculty.IsNullOrEmpty()) &&
		(i.SpecialtyNumber == info.SpecialtyNumber || info.SpecialtyNumber == "None"|| info.Faculty.IsNullOrEmpty()) &&
		(i.SPZ == info.SPZ || info.SPZ == "None"|| info.SPZ.IsNullOrEmpty()) &&
		(i.FO == info.FO || info.FO == "None"|| info.FO.IsNullOrEmpty()) &&
		(i.GroupName == info.GroupName || info.GroupName == "None"|| info.GroupName.IsNullOrEmpty()) &&
		(i.Name == info.Name || info.Name == "None"|| info.Name.IsNullOrEmpty()) &&
		(i.NumberOfDepartament == info.NumberOfDepartament || info.NumberOfDepartament == "None"|| info.NumberOfDepartament.IsNullOrEmpty()) &&
		(i.TypeOfCourseProject == info.TypeOfCourseProject || info.TypeOfCourseProject == "None"|| info.TypeOfCourseProject.IsNullOrEmpty()) &&
		(i.CountOfHourLecture == info.CountOfHourLecture || info.CountOfHourLecture == "None"|| info.CountOfHourLecture.IsNullOrEmpty()) &&
		(i.CountOfHourPractice == info.CountOfHourPractice || info.CountOfHourPractice == "None"|| info.CountOfHourPractice.IsNullOrEmpty()) &&
		(i.CountOfHourLab == info.CountOfHourLab || info.CountOfHourLab == "None"|| info.CountOfHourLab.IsNullOrEmpty()) &&
		(i.CountOfHourCourseProject == info.CountOfHourCourseProject || info.CountOfHourCourseProject == "None"|| info.CountOfHourCourseProject.IsNullOrEmpty()) &&
		(i.CountOfHourCourseWork == info.CountOfHourCourseWork || info.CountOfHourCourseWork == "None"|| info.CountOfHourCourseWork.IsNullOrEmpty()) &&
		(i.ExamHours == info.ExamHours || info.ExamHours == "None"|| info.Faculty.IsNullOrEmpty()) &&
		(i.SRS == info.SRS || info.SRS == "None"|| info.SRS.IsNullOrEmpty()) &&
		(i.CountOfHourCourseWork == info.CountOfHourCourseWork || info.CountOfHourCourseWork == "None"|| info.CountOfHourCourseWork.IsNullOrEmpty()) &&
		(i.TypeOfControl == info.TypeOfControl || info.TypeOfControl == "None"|| info.TypeOfControl.IsNullOrEmpty())
		);
		
		return list;
	}
}
