using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BTCapp.Migrations
{
    public partial class Newmigration2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Prices",
                table: "Prices");

            migrationBuilder.RenameTable(
                name: "Prices",
                newName: "btcprice");

            migrationBuilder.RenameIndex(
                name: "IX_Prices_TimePoint",
                table: "btcprice",
                newName: "IX_btcprice_TimePoint");

            migrationBuilder.AlterColumn<DateTime>(
                name: "TimePoint",
                table: "btcprice",
                type: "datetime(6)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2(6)");

            migrationBuilder.AlterColumn<float>(
                name: "CloseAmount",
                table: "btcprice",
                type: "float",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "real");

            migrationBuilder.AddPrimaryKey(
                name: "PK_btcprice",
                table: "btcprice",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_btcprice",
                table: "btcprice");

            migrationBuilder.RenameTable(
                name: "btcprice",
                newName: "Prices");

            migrationBuilder.RenameIndex(
                name: "IX_btcprice_TimePoint",
                table: "Prices",
                newName: "IX_Prices_TimePoint");

            migrationBuilder.AlterColumn<DateTime>(
                name: "TimePoint",
                table: "Prices",
                type: "datetime2(6)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");

            migrationBuilder.AlterColumn<double>(
                name: "CloseAmount",
                table: "Prices",
                type: "real",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "float");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Prices",
                table: "Prices",
                column: "Id");
        }
    }
}
