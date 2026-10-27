using System.Reflection;
using Microsoft.EntityFrameworkCore;
using PosEnterprise.Core.Entities;

namespace PosEnterprise.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
	public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
		: base(options)
	{
	}

	public DbSet<BlogPost> BlogPosts => Set<BlogPost>();
	public DbSet<ContactRequest> ContactRequests => Set<ContactRequest>();

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);
		modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
	}
}