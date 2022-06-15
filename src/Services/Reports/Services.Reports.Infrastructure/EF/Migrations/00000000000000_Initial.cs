using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Services.Reports.Infrastructure.EF.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "rpd");

            migrationBuilder.CreateTable(
                name: "Reports",
                schema: "rpd",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    NumberOfContacts = table.Column<long>(type: "bigint", nullable: false),
                    NumberOfPhoneNumbers = table.Column<long>(type: "bigint", nullable: false),
                    Location = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reports", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reports_Location",
                schema: "rpd",
                table: "Reports",
                column: "Location",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reports",
                schema: "rpd");
        }
    }
}
