using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankTransferApp.Infrastructure.Migrations;

/// <inheritdoc />
public partial class AddSystemUser : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.InsertData(
            table: "users",
            columns: ["Id", "FirstName", "LastName", "Password", "CreatedAt", "CreatedById", "IsActive"],
            values: ["00000000-0000-0000-0000-000000000000", "System", "User", "$2a$11$/m7uXZZM9.y24i04cVAG0O56jvBeQaTSOnj48tNGaIKlD1I5t54tK", DateTime.UtcNow, "00000000-0000-0000-0000-000000000000", true]);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DeleteData(
            table: "users",
            keyColumn: "Id",
            keyValue: "00000000-0000-0000-0000-000000000000");
    }
}
