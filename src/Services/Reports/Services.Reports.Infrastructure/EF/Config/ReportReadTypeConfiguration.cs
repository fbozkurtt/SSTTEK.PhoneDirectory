using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Services.Reports.Infrastructure.EF.Models;

namespace Services.Reports.Infrastructure.EF.Config;

internal sealed class ReportReadTypeConfiguration : IEntityTypeConfiguration<ReportReadModel>
{
    public void Configure(EntityTypeBuilder<ReportReadModel> builder)
    {
        builder.ToTable("Reports");
        builder.HasKey("Id");

        builder
            .Property(r => r.Location)
            .HasConversion(l => l.ToString(), l => new ReportLocationReadModel(l!));

        builder.HasIndex(r => r.Location)
            .IsUnique();
    }
}