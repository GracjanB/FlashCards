using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FlashCards.Data.Migrations
{
    public partial class ChangedSizeOfPasswordHashAndSaltColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "PasswordSalt",
                table: "Users",
                type: "BINARY(64)",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "BINARY(512)");

            migrationBuilder.AlterColumn<byte[]>(
                name: "PasswordHash",
                table: "Users",
                type: "BINARY(64)",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "BINARY(512)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "PasswordSalt",
                table: "Users",
                type: "BINARY(512)",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "BINARY(64)");

            migrationBuilder.AlterColumn<byte[]>(
                name: "PasswordHash",
                table: "Users",
                type: "BINARY(512)",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "BINARY(64)");
        }
    }
}
