using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace RPDSerice.Models;
public class RPD
{
	[Key] [JsonIgnore]
	public int Id { get; set; }
	[JsonIgnore]
	public int CreatorId { get; set; }
	[JsonIgnore]
	public int CriticalInfoId { get; set; }
	[JsonIgnore]
	public int RpdInfoId { get; set; }
	public required CriticalInfo CriticalInfo { get; set; }
	public RpdInfo? RpdInfo { get; set; }
}

public class CriticalInfo
{
	[Key] [JsonIgnore]
	public int Id { get; set; }
	public string? Faculty { get; set; }
	public string? Zach { get; set; }

	public string? SpecialtyNumber { get; set; }
	public string? SPZ { get; set; }
	public string? FO { get; set; }
	public string? GroupName { get; set; }
	public string? Name { get; set; }
	public string? NumberOfDepartament { get; set; }
	public string? TypeOfCourseProject { get; set; }
	public string? CountOfHourLecture { get; set; }
	public string? CountOfHourPractice { get; set; }
	public string? CountOfHourLab { get; set; }
	public string? CountOfHourCourseProject { get; set; }
	public string? CountOfHourCourseWork { get; set; }
	public string? ExamHours { get; set; }
	public string? SRS { get; set; }
	public string? TypeOfControl { get; set; }
	
	public static string GetTypeOfExam(bool Test, bool DiffTest, bool KandExam, bool Exam)
	{
		if (Test) return "test";
		if (DiffTest) return "difftest";
		if (KandExam) return "kandexam";
		if (Exam) return "exam";
		throw new ArgumentException("All types are false");
	}
	
	

}

public class RpdInfo
{
	[JsonIgnore]
	public int Id { get; set; }
	public string CharacteristicsOfTheSubjectArea { get; set; }
	public string LearningGoals { get; set; }
	public string RequaredOrNotRequiared { get; set; }



}