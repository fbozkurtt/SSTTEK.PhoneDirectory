using Microsoft.EntityFrameworkCore;
using Services.Reports.Infrastructure.EF.Config;
using Services.Reports.Infrastructure.EF.Models;

namespace Services.Reports.Infrastructure.EF.Contexts
{
    internal sealed class ReadDbContext : DbContext
    {
        public DbSet<ReportReadModel> Reports { get; set; } = null!;

        public ReadDbContext(DbContextOptions<ReadDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("rpd");

            var configuration = new ReportReadTypeConfiguration();
            modelBuilder.ApplyConfiguration(configuration);
        }
    }
}