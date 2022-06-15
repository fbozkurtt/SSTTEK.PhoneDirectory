using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Services.Contacts.Infrastructure.EF.Models;

namespace Services.Contacts.Infrastructure.EF.Config;

internal sealed class ContactReadTypeConfiguration : IEntityTypeConfiguration<ContactReadModel>,
    IEntityTypeConfiguration<ContactFieldReadModel>
{
    public void Configure(EntityTypeBuilder<ContactFieldReadModel> builder)
    {
        builder.ToTable("ContactFields");
    }

    public void Configure(EntityTypeBuilder<ContactReadModel> builder)
    {
        builder.ToTable("Contacts");
        builder.HasKey("Id");

        builder
            .HasMany(pl => pl.Fields)
            .WithOne(pi => pi.Contact);
    }
}