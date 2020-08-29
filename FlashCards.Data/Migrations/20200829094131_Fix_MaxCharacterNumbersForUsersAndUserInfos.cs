using Microsoft.EntityFrameworkCore.Migrations;

namespace FlashCards.Data.Migrations
{
    public partial class Fix_MaxCharacterNumbersForUsersAndUserInfos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                type: "NVARCHAR(128)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "NVARCHAR",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "UserInfos",
                type: "NVARCHAR(64)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "NVARCHAR",
                oldMaxLength: 64,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "UserInfos",
                type: "NVARCHAR(64)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "NVARCHAR",
                oldMaxLength: 64,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DisplayName",
                table: "UserInfos",
                type: "NVARCHAR(32)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "NVARCHAR",
                oldMaxLength: 32,
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                type: "NVARCHAR",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "NVARCHAR(128)");

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "UserInfos",
                type: "NVARCHAR",
                maxLength: 64,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "NVARCHAR(64)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "UserInfos",
                type: "NVARCHAR",
                maxLength: 64,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "NVARCHAR(64)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DisplayName",
                table: "UserInfos",
                type: "NVARCHAR",
                maxLength: 32,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "NVARCHAR(32)",
                oldNullable: true);
        }
    }
}
