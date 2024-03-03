using System.ComponentModel.DataAnnotations;

namespace RPDSerice.Models;
public class RPD : RPDBase
{
	[Key]
	public int Id { get; set; }
	public int CriticalInfoId { get; set; }
	public int RpdlInfoId { get; set; }
	public CriticalInfo CriticalInfo { get; set; }
	public RpdInfo RpdInfo { get; set; }
}

public class CriticalInfo
{
	[Key]
	public int Id { get; set; }
	public string Faculty { get; set; }
	public string SpecialtyNumber { get; set; }
	public string SPZ { get; set; }
	public string FO { get; set; }
	public string GroupName { get; set; }
	public string Name { get; set; }
	public string NumberOfDepartament { get; set; }
	public string TypeOfCourseProject { get; set; }
	public string CountOfHourLecture { get; set; }
	public string CountOfHourPractice { get; set; }
	public string CountOfHourLab { get; set; }
	public string CountOfHourCourseProject { get; set; }
	public string CountOfHourCourseWork { get; set; }
	public string ExamHours { get; set; }
	public string SRS { get; set; }
	public bool Test { get; set; }
	public bool DiffTest { get; set; }
	public bool KandExam { get; set; }
	public bool Exam { get; set; }


}

public class RpdInfo
{
	public int Id { get; set; }

}