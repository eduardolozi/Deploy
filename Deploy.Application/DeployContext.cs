using Deploy.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Deploy.Application;

public class DeployContext : DbContext
{
    public DbSet<Project> Project { get; set; }
    public DbSet<Build> Build { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(Environment.GetEnvironmentVariable("DEPLOY_DATABASE_CONNECTION"));
    }
}