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
                    RecipientName = table.Column<string>(maxLength: 150, nullable: true),
                    RecipientAddress = table.Column<string>(nullable: true),
                    RecipientEmail = table.Column<string>(maxLength: 100, nullable: true),
                    RecipientPhoneNumber = table.Column<string>(maxLength: 15, nullable: true),
                    OrderNumber = table.Column<string>(maxLength: 20, nullable: true),
                    Sender = table.Column<string>(maxLength: 200, nullable: true),
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
                    Email = table.Column<string>(maxLength: 100, nullable: true),
                    PasswordHash = table.Column<string>(nullable: true),
                    Role = table.Column<int>(nullable: false),
                    UserConsumerMarketId = table.Column<int>(nullable: true),
                    PartnerId = table.Column<int>(maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Identities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Subscribers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    CreatedBy = table.Column<int>(nullable: true),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<int>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    PartnerId = table.Column<int>(nullable: false),
                    NotificationUrl = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subscribers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Subscriptions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    CreatedBy = table.Column<int>(nullable: true),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<int>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    SubscriberId = table.Column<int>(nullable: false),
                    EventType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subscriptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Subscriptions_Subscribers_SubscriberId",
                        column: x => x.SubscriberId,
                        principalTable: "Subscribers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Identities",
                columns: new[] { "Id", "CreatedBy", "Email", "IsDeleted", "PartnerId", "PasswordHash", "Role", "UpdatedAt", "UpdatedBy", "UserConsumerMarketId" },
                values: new object[] { 1, null, "admin@admin.com", false, null, "5BAA61E4C9B93F3F0682250B6CF8331B7EE68FD8", 0, null, null, null });

            migrationBuilder.InsertData(
                table: "Identities",
                columns: new[] { "Id", "CreatedBy", "Email", "IsDeleted", "PartnerId", "PasswordHash", "Role", "UpdatedAt", "UpdatedBy", "UserConsumerMarketId" },
                values: new object[] { 2, null, "partner@partner.com", false, 222, "5BAA61E4C9B93F3F0682250B6CF8331B7EE68FD8", 2, null, null, null });

            migrationBuilder.InsertData(
                table: "Identities",
                columns: new[] { "Id", "CreatedBy", "Email", "IsDeleted", "PartnerId", "PasswordHash", "Role", "UpdatedAt", "UpdatedBy", "UserConsumerMarketId" },
                values: new object[] { 3, null, "user@user.com", false, null, "5BAA61E4C9B93F3F0682250B6CF8331B7EE68FD8", 1, null, null, 333 });

            migrationBuilder.InsertData(
                table: "Subscribers",
                columns: new[] { "Id", "CreatedBy", "IsDeleted", "NotificationUrl", "PartnerId", "UpdatedAt", "UpdatedBy" },
                values: new object[] { 1, null, false, "https://partner222.com/webhooks", 222, null, null });

            migrationBuilder.InsertData(
                table: "Subscriptions",
                columns: new[] { "Id", "CreatedBy", "EventType", "IsDeleted", "SubscriberId", "UpdatedAt", "UpdatedBy" },
                values: new object[] { 1, null, 0, false, 1, null, null });

            migrationBuilder.InsertData(
                table: "Subscriptions",
                columns: new[] { "Id", "CreatedBy", "EventType", "IsDeleted", "SubscriberId", "UpdatedAt", "UpdatedBy" },
                values: new object[] { 2, null, 1, false, 1, null, null });

            migrationBuilder.CreateIndex(
                name: "IX_Identities_Email",
                table: "Identities",
                column: "Email");

            migrationBuilder.CreateIndex(
                name: "IX_Identities_PartnerId",
                table: "Identities",
                column: "PartnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_SubscriberId",
                table: "Subscriptions",
                column: "SubscriberId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Deliveries");

            migrationBuilder.DropTable(
                name: "Identities");

            migrationBuilder.DropTable(
                name: "Subscriptions");

            migrationBuilder.DropTable(
                name: "Subscribers");
        }
    }
}
