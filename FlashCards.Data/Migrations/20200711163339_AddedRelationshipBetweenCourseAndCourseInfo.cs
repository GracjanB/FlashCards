using Microsoft.EntityFrameworkCore.Migrations;

namespace FlashCards.Data.Migrations
{
    public partial class AddedRelationshipBetweenCourseAndCourseInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CourseId",
                table: "CourseInfos",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CourseInfos_CourseId",
                table: "CourseInfos",
                column: "CourseId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseInfos_Courses_CourseId",
                table: "CourseInfos",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseInfos_Courses_CourseId",
                table: "CourseInfos");

            migrationBuilder.DropIndex(
                name: "IX_CourseInfos_CourseId",
                table: "CourseInfos");

            migrationBuilder.DropColumn(
                name: "CourseId",
                table: "CourseInfos");
        }
    }
}
