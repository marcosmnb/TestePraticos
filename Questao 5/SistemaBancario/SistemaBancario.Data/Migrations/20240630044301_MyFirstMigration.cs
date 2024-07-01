using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SistemaBancario.Data.Migrations
{
    /// <inheritdoc />
    public partial class MyFirstMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ContaCorrente",
                columns: table => new
                {
                    IdContaCorrente = table.Column<string>(type: "TEXT", nullable: false),
                    Numero = table.Column<int>(type: "INTEGER", nullable: false),
                    Nome = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Ativo = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContaCorrente", x => x.IdContaCorrente);
                });

            migrationBuilder.CreateTable(
                name: "Idempotencia",
                columns: table => new
                {
                    ChaveIdempotencia = table.Column<string>(type: "TEXT", nullable: false),
                    Requisicao = table.Column<string>(type: "TEXT", nullable: false),
                    Resultado = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Idempotencia", x => x.ChaveIdempotencia);
                });

            migrationBuilder.CreateTable(
                name: "Movimento",
                columns: table => new
                {
                    IdMovimento = table.Column<string>(type: "TEXT", nullable: false),
                    IdContaCorrente = table.Column<string>(type: "TEXT", nullable: false),
                    DataMovimento = table.Column<string>(type: "TEXT", nullable: false),
                    TipoMovimento = table.Column<string>(type: "TEXT", maxLength: 1, nullable: false),
                    Valor = table.Column<decimal>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movimento", x => x.IdMovimento);
                    table.ForeignKey(
                        name: "FK_Movimento_ContaCorrente_IdContaCorrente",
                        column: x => x.IdContaCorrente,
                        principalTable: "ContaCorrente",
                        principalColumn: "IdContaCorrente",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ContaCorrente",
                columns: new[] { "IdContaCorrente", "Ativo", "Nome", "Numero" },
                values: new object[,]
                {
                    { "382D323D-7067-ED11-8866-7D5DFA4A16C9", 1, "Tevin Mcconnell", 789 },
                    { "B6BAFC09-6967-ED11-A567-055DFA4A16C9", 1, "Katherine Sanchez", 123 },
                    { "BCDACA4A-7067-ED11-AF81-825DFA4A16C9", 0, "Jarrad Mckee", 852 },
                    { "D2E02051-7067-ED11-94C0-835DFA4A16C9", 0, "Elisha Simons", 963 },
                    { "F475F943-7067-ED11-A06B-7E5DFA4A16C9", 0, "Ameena Lynn", 741 },
                    { "FA99D033-7067-ED11-96C6-7C5DFA4A16C9", 1, "Eva Woodward", 456 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Movimento_IdContaCorrente",
                table: "Movimento",
                column: "IdContaCorrente");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Idempotencia");

            migrationBuilder.DropTable(
                name: "Movimento");

            migrationBuilder.DropTable(
                name: "ContaCorrente");
        }
    }
}
