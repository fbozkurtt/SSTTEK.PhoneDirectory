using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Services.Reports.Domain.Entities.Report;
using Services.Reports.Domain.Entities.Report.ValueObjects;

namespace Services.Reports.Infrastructure.EF.Config;

internal sealed class ReportTypeConfiguration : IEntityTypeConfiguration<Report>
{
    public void Configure(EntityTypeBuilder<Report> builder)
    {
        builder.HasKey(c => c.Id);

        var reportLocationConverter = new ValueConverter<ReportLocation, string>(
            r => r.Value,
            r => new ReportLocation(r));

        builder.Property(c => c.Id)
            .HasConversion(id => id.Value, id => new ReportId(id));

        builder.Property(typeof(ReportLocation), "_location")
            .HasConversion(reportLocationConverter)
            .HasColumnName("Location")
            .IsRequired();

        builder.Property(typeof(uint), "_numberOfContacts")
            .HasColumnName("NumberOfContacts");

        builder.Property(typeof(uint), "_numberOfPhoneNumbers")
            .HasColumnName("NumberOfPhoneNumbers");

        builder.ToTable("Reports");
    }
}