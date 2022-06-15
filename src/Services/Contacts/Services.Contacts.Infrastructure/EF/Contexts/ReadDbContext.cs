using Microsoft.EntityFrameworkCore;
using Services.Contacts.Infrastructure.EF.Config;
using Services.Contacts.Infrastructure.EF.Models;

namespace Services.Contacts.Infrastructure.EF.Contexts;

internal sealed class ReadDbContext : DbContext
{
    public ReadDbContext(DbContextOptions<ReadDbContext> options) : base(options)
    {
    }

    public DbSet<ContactReadModel> Contacts { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("pd");

        var configuration = new ContactReadTypeConfiguration();
        modelBuilder.ApplyConfiguration<ContactReadModel>(configuration);
        modelBuilder.ApplyConfiguration<ContactFieldReadModel>(configuration);
    }
}