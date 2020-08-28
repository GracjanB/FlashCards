using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FlashCards.Data.Migrations
{
    public partial class AddedConfigurationForPasswordSaltAndHashInUser4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "PasswordSalt",
                table: "Users",
                type: "BINARY(512)",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "VARBINARY(MAX)");

            migrationBuilder.AlterColumn<byte[]>(
                name: "PasswordHash",
                table: "Users",
                type: "BINARY(512)",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "VARBINARY(MAX)");

            migrationBuilder.AlterColumn<string>(
                name: "Country",
                table: "UserInfos",
                type: "NVARCHAR(64)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "NVARCHAR",
                oldMaxLength: 64,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "City",
                table: "UserInfos",
                type: "NVARCHAR(64)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "NVARCHAR",
                oldMaxLength: 64,
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "PasswordSalt",
                table: "Users",
                type: "VARBINARY(MAX)",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "BINARY(512)");

            migrationBuilder.AlterColumn<byte[]>(
                name: "PasswordHash",
                table: "Users",
                type: "VARBINARY(MAX)",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "BINARY(512)");

            migrationBuilder.AlterColumn<string>(
                name: "Country",
                table: "UserInfos",
                type: "NVARCHAR",
                maxLength: 64,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "NVARCHAR(64)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "City",
                table: "UserInfos",
                type: "NVARCHAR",
                maxLength: 64,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "NVARCHAR(64)",
                oldNullable: true);
        }
    }
}
