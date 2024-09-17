using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api.Migrations.Tenant
{
    /// <inheritdoc />
    public partial class UpdateTenant : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SubscriptionLevel",
                table: "Tenants",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SubscriptionLevel",
                table: "Tenants");
        }
    }
}
