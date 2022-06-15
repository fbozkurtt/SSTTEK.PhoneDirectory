using Microsoft.EntityFrameworkCore;
using Services.Contacts.Domain.Aggregates.Contact;
using Services.Contacts.Domain.Aggregates.Contact.ValueObjects;
using Services.Contacts.Infrastructure.EF.Config;

namespace Services.Contacts.Infrastructure.EF.Contexts
{
    internal sealed class WriteDbContext : DbContext
    {
        public DbSet<Contact> Contacts { get; set; } = null!;

        public WriteDbContext(DbContextOptions<WriteDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("pd");
            
            var configuration = new ContactTypeConfiguration();
            modelBuilder.ApplyConfiguration<Contact>(configuration);
            modelBuilder.ApplyConfiguration<ContactField>(configuration);
        }
    }
}