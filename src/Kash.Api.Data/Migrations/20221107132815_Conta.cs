using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kash.Api.Data.Migrations
{
#pragma warning disable CS1591
    public partial class Conta : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Contas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false),
                    Numero = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true),
                    Agencia = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true),
                    Saldo = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BancoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TipoContaId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Ativo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contas_Bancos_BancoId",
                        column: x => x.BancoId,
                        principalTable: "Bancos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Contas_TiposConta_TipoContaId",
                        column: x => x.TipoContaId,
                        principalTable: "TiposConta",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contas_BancoId",
                table: "Contas",
                column: "BancoId");

            migrationBuilder.CreateIndex(
                name: "IX_Contas_TipoContaId",
                table: "Contas",
                column: "TipoContaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contas");
        }
    }
#pragma warning restore CS1591
}
