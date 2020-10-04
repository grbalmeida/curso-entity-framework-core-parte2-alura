using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Alura.Filmes.App.Migrations
{
    public partial class Enumerado : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "rating",
                table: "film",
                type: "varchar(10)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(10)",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "rating",
                table: "film",
                type: "varchar(10)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "varchar(10)");
        }
    }
}
