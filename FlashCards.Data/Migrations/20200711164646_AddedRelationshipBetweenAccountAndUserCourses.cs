using Microsoft.EntityFrameworkCore.Migrations;

namespace FlashCards.Data.Migrations
{
    public partial class AddedRelationshipBetweenAccountAndUserCourses : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AccountId",
                table: "UserCourses",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_UserCourses_AccountId",
                table: "UserCourses",
                column: "AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserCourses_Accounts_AccountId",
                table: "UserCourses",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserCourses_Accounts_AccountId",
                table: "UserCourses");

            migrationBuilder.DropIndex(
                name: "IX_UserCourses_AccountId",
                table: "UserCourses");

            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "UserCourses");
        }
    }
}
