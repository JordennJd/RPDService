using RPDSerice.Models;

namespace RPDService.Dtos;

public class ChangeCriticalDto
{
	public CriticalInfo source { get; set; }
	public CriticalInfo des { get; set; }
}

public class ChangeRpdInfoDtoByCriticalInfo
{
	public CriticalInfo source { get; set; }
	public RpdInfo des { get; set; }
}
public class ChangeRpdInfoDtoByRpd
{
	public RPD source { get; set; }
	public RpdInfo des { get; set; }
}