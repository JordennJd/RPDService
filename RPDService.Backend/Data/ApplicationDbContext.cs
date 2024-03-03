using Microsoft.EntityFrameworkCore;
using RPDSerice.Models;

namespace RPDSerice;

public class ApplicationDbContext : DbContext
{
	public DbSet<RPD> RPDs {get;set;}
	public DbSet<CriticalInfo> CriticalInfo { get; set; }
	public DbSet<RpdInfo> RpdInfo { get; set; }

	public DbSet<Flags> Flags { get; set; }
	public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
	{

	}
	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<Flags>().HasNoKey();
	}
}
