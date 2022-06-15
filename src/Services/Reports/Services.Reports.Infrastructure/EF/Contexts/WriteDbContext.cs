using Microsoft.EntityFrameworkCore;
using Services.Reports.Domain.Entities.Report;
using Services.Reports.Infrastructure.EF.Config;

namespace Services.Reports.Infrastructure.EF.Contexts;

internal sealed class WriteDbContext : DbContext
{
    public WriteDbContext(DbContextOptions<WriteDbContext> options) : base(options)
    {
    }

    public DbSet<Report> Reports { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("rpd");

        var configuration = new ReportTypeConfiguration();
        modelBuilder.ApplyConfiguration(configuration);
    }
}