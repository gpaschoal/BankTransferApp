using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankTransferApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CreateUserTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    City = table.Column<string>(type: "char(100)", maxLength: 100, nullable: false),
                    State = table.Column<string>(type: "char(50)", maxLength: 50, nullable: false),
                    Street = table.Column<string>(type: "char(200)", maxLength: 200, nullable: false),
                    ZipCode = table.Column<string>(type: "char(20)", maxLength: 20, nullable: false),
                    CellphoneAreaCode = table.Column<string>(type: "char(3)", maxLength: 3, nullable: false),
                    CellphoneNumber = table.Column<string>(type: "char(10)", maxLength: 10, nullable: false),
                    Cpf = table.Column<string>(type: "char(11)", maxLength: 11, nullable: false),
                    HomePhoneAreaCode = table.Column<string>(type: "char(3)", maxLength: 3, nullable: false),
                    HomePhoneNumber = table.Column<string>(type: "char(10)", maxLength: 10, nullable: false),
                    FirstName = table.Column<string>(type: "char(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "char(100)", maxLength: 100, nullable: false),
                    Password = table.Column<string>(type: "varchar", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
