using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Services.Contacts.Infrastructure.EF.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "pd");

            migrationBuilder.CreateTable(
                name: "Contacts",
                schema: "pd",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Version = table.Column<int>(type: "integer", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ContactFields",
                schema: "pd",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    ContactId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactFields", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContactFields_Contacts_ContactId",
                        column: x => x.ContactId,
                        principalSchema: "pd",
                        principalTable: "Contacts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContactFields_ContactId",
                schema: "pd",
                table: "ContactFields",
                column: "ContactId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContactFields",
                schema: "pd");

            migrationBuilder.DropTable(
                name: "Contacts",
                schema: "pd");
        }
    }
}
