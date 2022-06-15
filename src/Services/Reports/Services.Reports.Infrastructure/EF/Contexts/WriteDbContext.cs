using Microsoft.EntityFrameworkCore;
using Services.Reports.Domain.Entities.Report;
using Services.Reports.Infrastructure.EF.Config;

namespace Services.Reports.Infrastructure.EF.Contexts
{
    internal sealed class WriteDbContext : DbContext
    {
        public DbSet<Report> Reports { get; set; } = null!;

        public WriteDbContext(DbContextOptions<WriteDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("rpd");
            
            var configuration = new ReportTypeConfiguration();
            modelBuilder.ApplyConfiguration(configuration);
        }
    }
}