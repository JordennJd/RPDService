using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RPDSerice.Models;

namespace RPDSerice.RpdRepository.SearchEngine;
public class RpdSearchEngine
{
	private readonly ApplicationDbContext _context;

	public RpdSearchEngine(ApplicationDbContext context)
	{
		_context = context;
	}
	public IEnumerable<RPD> SearchRpdByCriticalInfo(CriticalInfo info)
	{
		var list = _context.RPDs.Include(r => r.RpdInfo).Include(r => r.CriticalInfo).Where(i =>
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

	public IEnumerable<RPD> SearchRpdByRpd(RPD rpd)
	{
		var info = rpd.CriticalInfo;
		var RpdInfo = rpd.RpdInfo;
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
		(i.CriticalInfo.TypeOfControl == info.TypeOfControl || info.TypeOfControl == "None" || info.TypeOfControl.IsNullOrEmpty()) &&

		(i.RpdInfo == null ? true : (i.RpdInfo.LearningGoals == RpdInfo.LearningGoals || RpdInfo.LearningGoals == "None" || RpdInfo.LearningGoals.IsNullOrEmpty()))&&
		(i.RpdInfo == null ? true : (i.RpdInfo.CharacteristicsOfTheSubjectArea == RpdInfo.CharacteristicsOfTheSubjectArea || RpdInfo.CharacteristicsOfTheSubjectArea == "None" || RpdInfo.CharacteristicsOfTheSubjectArea.IsNullOrEmpty()))&&
		(i.RpdInfo == null ? true : (i.RpdInfo.RequaredOrNotRequiared == RpdInfo.RequaredOrNotRequiared || RpdInfo.RequaredOrNotRequiared == "None" || RpdInfo.RequaredOrNotRequiared.IsNullOrEmpty()))
		);

		return list.ToList();
	}

	public IEnumerable<CriticalInfo> SearchCriticalInfoByCriticalInfo(CriticalInfo info)
	{
		var list = _context.CriticalInfo.Where(i =>
		(i.Faculty == info.Faculty || info.Faculty == "None" || info.Faculty.IsNullOrEmpty()) &&
		(i.SpecialtyNumber == info.SpecialtyNumber || info.SpecialtyNumber == "None" || info.SpecialtyNumber.IsNullOrEmpty()) &&
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
		(i.ExamHours == info.ExamHours || info.ExamHours == "None" || info.ExamHours.IsNullOrEmpty()) &&
		(i.SRS == info.SRS || info.SRS == "None" || info.SRS.IsNullOrEmpty()) &&
		(i.TypeOfControl == info.TypeOfControl || info.TypeOfControl == "None" || info.TypeOfControl.IsNullOrEmpty())
		);
		var temp =  list.ToList();
		return temp;
	}
}