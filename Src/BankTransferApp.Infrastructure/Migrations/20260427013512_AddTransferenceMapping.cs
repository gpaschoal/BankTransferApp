using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankTransferApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddTransferenceMapping : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "balance_per_month",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Balance = table.Column<decimal>(type: "numeric", nullable: false),
                    Year = table.Column<int>(type: "integer", nullable: false),
                    Month = table.Column<int>(type: "integer", nullable: false),
                    AccountId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_balance_per_month", x => x.Id);
                    table.ForeignKey(
                        name: "FK_balance_per_month_accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "accounts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "transfers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Value = table.Column<decimal>(type: "numeric", nullable: false),
                    Reference = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    SourceAccountId = table.Column<Guid>(type: "uuid", nullable: false),
                    DestinationAccountId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_transfers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_transfers_accounts_DestinationAccountId",
                        column: x => x.DestinationAccountId,
                        principalTable: "accounts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_transfers_accounts_SourceAccountId",
                        column: x => x.SourceAccountId,
                        principalTable: "accounts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "transactions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Value = table.Column<decimal>(type: "numeric", nullable: false),
                    Reference = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    AccountId = table.Column<Guid>(type: "uuid", nullable: false),
                    BalanceId = table.Column<Guid>(type: "uuid", nullable: false),
                    BalancePerMonthEntityId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_transactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_transactions_accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "accounts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_transactions_balance_per_month_BalanceId",
                        column: x => x.BalanceId,
                        principalTable: "balance_per_month",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_transactions_balance_per_month_BalancePerMonthEntityId",
                        column: x => x.BalancePerMonthEntityId,
                        principalTable: "balance_per_month",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "deposits",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Value = table.Column<decimal>(type: "numeric", nullable: false),
                    Reference = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    AccountId = table.Column<Guid>(type: "uuid", nullable: false),
                    TransactionId = table.Column<Guid>(type: "uuid", nullable: false)
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
                    Value = table.Column<decimal>(type: "numeric", nullable: false),
                    Reference = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    AccountId = table.Column<Guid>(type: "uuid", nullable: false),
                    TransactionId = table.Column<Guid>(type: "uuid", nullable: false)
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
                name: "IX_balance_per_month_AccountId",
                table: "balance_per_month",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_deposits_AccountId",
                table: "deposits",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_deposits_TransactionId",
                table: "deposits",
                column: "TransactionId");

            migrationBuilder.CreateIndex(
                name: "IX_transactions_AccountId",
                table: "transactions",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_transactions_BalanceId",
                table: "transactions",
                column: "BalanceId");

            migrationBuilder.CreateIndex(
                name: "IX_transactions_BalancePerMonthEntityId",
                table: "transactions",
                column: "BalancePerMonthEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_transfers_DestinationAccountId",
                table: "transfers",
                column: "DestinationAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_transfers_SourceAccountId",
                table: "transfers",
                column: "SourceAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_withdrawals_AccountId",
                table: "withdrawals",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_withdrawals_TransactionId",
                table: "withdrawals",
                column: "TransactionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "deposits");

            migrationBuilder.DropTable(
                name: "transfers");

            migrationBuilder.DropTable(
                name: "withdrawals");

            migrationBuilder.DropTable(
                name: "transactions");

            migrationBuilder.DropTable(
                name: "balance_per_month");
        }
    }
}
