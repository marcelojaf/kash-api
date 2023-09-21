using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kash.Api.Data.Migrations
{
#pragma warning disable CS1591
    public partial class InitialDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bancos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false),
                    Numero = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bancos", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bancos");
        }
    }
#pragma warning restore CS1591
}
