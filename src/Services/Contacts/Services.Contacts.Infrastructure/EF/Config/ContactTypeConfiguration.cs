using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Services.Contacts.Domain.Aggregates.Contact;
using Services.Contacts.Domain.Aggregates.Contact.ValueObjects;

namespace Services.Contacts.Infrastructure.EF.Config;

internal sealed class ContactTypeConfiguration : IEntityTypeConfiguration<Contact>,
    IEntityTypeConfiguration<ContactField>
{
    public void Configure(EntityTypeBuilder<Contact> builder)
    {
        builder.HasKey(c => c.Id);

        var contactFirstNameConverter = new ValueConverter<ContactFirstName, string>(
            fn => fn.Value,
            fn => new ContactFirstName(fn));

        var contactLastNameConverter = new ValueConverter<ContactLastName, string>(
            ln => ln.Value,
            ln => new ContactLastName(ln));

        builder.Property(c => c.Id)
            .HasConversion(id => id.Value, id => new ContactId(id));

        builder.Property(typeof(ContactFirstName), "_firstName")
            .HasConversion(contactFirstNameConverter)
            .HasColumnName("FirstName")
            .IsRequired();

        builder.Property(typeof(ContactLastName), "_lastName")
            .HasConversion(contactLastNameConverter)
            .HasColumnName("LastName");

        builder.HasMany(typeof(ContactField), "_fields")
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);

        builder.ToTable("Contacts");
    }

    public void Configure(EntityTypeBuilder<ContactField> builder)
    {
        builder.Property<Guid>("Id");
        builder.Property(cf => cf.Value);
        builder.Property(cf => cf.Type);
        builder.ToTable("ContactFields");
    }
}