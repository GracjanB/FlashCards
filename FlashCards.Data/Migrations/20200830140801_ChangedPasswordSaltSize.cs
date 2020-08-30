using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FlashCards.Data.Migrations
{
    public partial class ChangedPasswordSaltSize : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "PasswordSalt",
                table: "Users",
                type: "BINARY(128)",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "BINARY(64)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "PasswordSalt",
                table: "Users",
                type: "BINARY(64)",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "BINARY(128)");
        }
    }
}
