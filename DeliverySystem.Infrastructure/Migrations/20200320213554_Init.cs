using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DeliverySystem.Infrastructure.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Deliveries",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    CreatedBy = table.Column<int>(nullable: true),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<int>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    State = table.Column<int>(nullable: false),
                    AccessWindowStart = table.Column<DateTime>(nullable: false),
                    AccessWindowEnd = table.Column<DateTime>(nullable: false),
                    RecipientName = table.Column<string>(nullable: true),
                    RecipientAddress = table.Column<string>(nullable: true),
                    RecipientEmail = table.Column<string>(nullable: true),
                    RecipientPhoneNumber = table.Column<string>(nullable: true),
                    OrderNumber = table.Column<string>(nullable: true),
                    Sender = table.Column<string>(nullable: true),
                    UserId = table.Column<int>(nullable: false),
                    PartnerId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Deliveries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Identities",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    CreatedBy = table.Column<int>(nullable: true),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<int>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    PasswordHash = table.Column<string>(nullable: true),
                    Role = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Identities", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Identities",
                columns: new[] { "Id", "CreatedBy", "Email", "IsDeleted", "PasswordHash", "Role", "UpdatedAt", "UpdatedBy" },
                values: new object[] { 1, null, "admin@admin.com", false, "5BAA61E4C9B93F3F0682250B6CF8331B7EE68FD8", 0, null, null });

            migrationBuilder.InsertData(
                table: "Identities",
                columns: new[] { "Id", "CreatedBy", "Email", "IsDeleted", "PasswordHash", "Role", "UpdatedAt", "UpdatedBy" },
                values: new object[] { 2, null, "partner@partner.com", false, "5BAA61E4C9B93F3F0682250B6CF8331B7EE68FD8", 2, null, null });

            migrationBuilder.InsertData(
                table: "Identities",
                columns: new[] { "Id", "CreatedBy", "Email", "IsDeleted", "PasswordHash", "Role", "UpdatedAt", "UpdatedBy" },
                values: new object[] { 3, null, "user@user.com", false, "5BAA61E4C9B93F3F0682250B6CF8331B7EE68FD8", 1, null, null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Deliveries");

            migrationBuilder.DropTable(
                name: "Identities");
        }
    }
}
