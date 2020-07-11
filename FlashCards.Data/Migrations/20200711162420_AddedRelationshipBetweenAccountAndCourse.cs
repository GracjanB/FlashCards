using Microsoft.EntityFrameworkCore.Migrations;

namespace FlashCards.Data.Migrations
{
    public partial class AddedRelationshipBetweenAccountAndCourse : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AccountCreatedId",
                table: "Courses",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Courses_AccountCreatedId",
                table: "Courses",
                column: "AccountCreatedId");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Accounts_AccountCreatedId",
                table: "Courses",
                column: "AccountCreatedId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Accounts_AccountCreatedId",
                table: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_Courses_AccountCreatedId",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "AccountCreatedId",
                table: "Courses");
        }
    }
}
