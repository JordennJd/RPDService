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
}
