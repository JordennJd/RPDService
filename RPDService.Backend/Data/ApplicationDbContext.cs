using ExcelDataReader.Log;
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
		try
		{
			Database.EnsureCreated();

		}
		catch {
			Log.GetLoggerFor(typeof(ApplicationDbContext)).Info("Database is alerdy created");
		}
		// Database.Migrate();
	}
	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<Flags>().HasNoKey();
	}
}
