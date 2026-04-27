using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankTransferApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixDepositWithdrawLogic : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "deposits");

            migrationBuilder.DropTable(
                name: "withdrawals");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "deposits",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AccountId = table.Column<Guid>(type: "uuid", nullable: false),
                    TransactionId = table.Column<Guid>(type: "uuid", nullable: false),
                    Reference = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Value = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_deposits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_deposits_accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "accounts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_deposits_transactions_TransactionId",
                        column: x => x.TransactionId,
                        principalTable: "transactions",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "withdrawals",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AccountId = table.Column<Guid>(type: "uuid", nullable: false),
                    TransactionId = table.Column<Guid>(type: "uuid", nullable: false),
                    Reference = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Value = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_withdrawals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_withdrawals_accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "accounts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_withdrawals_transactions_TransactionId",
                        column: x => x.TransactionId,
                        principalTable: "transactions",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_deposits_AccountId",
                table: "deposits",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_deposits_TransactionId",
                table: "deposits",
                column: "TransactionId");

            migrationBuilder.CreateIndex(
                name: "IX_withdrawals_AccountId",
                table: "withdrawals",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_withdrawals_TransactionId",
                table: "withdrawals",
                column: "TransactionId");
        }
    }
}
