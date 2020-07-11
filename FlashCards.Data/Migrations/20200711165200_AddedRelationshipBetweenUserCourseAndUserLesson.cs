using Microsoft.EntityFrameworkCore.Migrations;

namespace FlashCards.Data.Migrations
{
    public partial class AddedRelationshipBetweenUserCourseAndUserLesson : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserCourseId",
                table: "UserLessons",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_UserLessons_UserCourseId",
                table: "UserLessons",
                column: "UserCourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserLessons_UserCourses_UserCourseId",
                table: "UserLessons",
                column: "UserCourseId",
                principalTable: "UserCourses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserLessons_UserCourses_UserCourseId",
                table: "UserLessons");

            migrationBuilder.DropIndex(
                name: "IX_UserLessons_UserCourseId",
                table: "UserLessons");

            migrationBuilder.DropColumn(
                name: "UserCourseId",
                table: "UserLessons");
        }
    }
}
