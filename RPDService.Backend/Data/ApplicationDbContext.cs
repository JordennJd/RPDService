using Microsoft.EntityFrameworkCore;

namespace RPDSerice;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
}
