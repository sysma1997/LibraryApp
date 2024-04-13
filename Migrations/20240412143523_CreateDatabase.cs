using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Library.Migrations
{
    /// <inheritdoc />
    public partial class CreateDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "TEXT", nullable: false),
                    cardNumber = table.Column<long>(type: "INTEGER", nullable: false),
                    name = table.Column<string>(type: "TEXT", nullable: false),
                    phone = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Libraries",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "TEXT", nullable: false),
                    name = table.Column<string>(type: "TEXT", nullable: false),
                    author = table.Column<string>(type: "TEXT", nullable: false),
                    numPages = table.Column<int>(type: "INTEGER", nullable: false),
                    quantity = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Libraries", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Loans",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "TEXT", nullable: false),
                    idLibrary = table.Column<Guid>(type: "TEXT", nullable: false),
                    idClient = table.Column<Guid>(type: "TEXT", nullable: false),
                    date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    deadline = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Loans", x => x.id);
                    table.ForeignKey(
                        name: "FK_Loans_Clients_idClient",
                        column: x => x.idClient,
                        principalTable: "Clients",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Loans_Libraries_idLibrary",
                        column: x => x.idLibrary,
                        principalTable: "Libraries",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Loans_idClient",
                table: "Loans",
                column: "idClient");

            migrationBuilder.CreateIndex(
                name: "IX_Loans_idLibrary",
                table: "Loans",
                column: "idLibrary");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Loans");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "Libraries");
        }
    }
}
