using Microsoft.EntityFrameworkCore;
using XYZStore.DataAccess;
using XYZStore.Models;
using XYZStore.Models.Models;

namespace XYZStore.DataAccess;

public class ApplicationDbContext : DbContext
{
	public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options) : base(options)
	{

	}
	public DbSet<Category>Categories { get; set; }
	public DbSet<CoverType>CoverType { get; set; }
	public DbSet<Product>Products { get; set; }
}
