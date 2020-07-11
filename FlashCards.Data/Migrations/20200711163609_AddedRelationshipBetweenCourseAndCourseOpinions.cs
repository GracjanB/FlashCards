using Microsoft.EntityFrameworkCore.Migrations;

namespace FlashCards.Data.Migrations
{
    public partial class AddedRelationshipBetweenCourseAndCourseOpinions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CourseId",
                table: "CourseOpinions",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CourseOpinions_CourseId",
                table: "CourseOpinions",
                column: "CourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseOpinions_Courses_CourseId",
                table: "CourseOpinions",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseOpinions_Courses_CourseId",
                table: "CourseOpinions");

            migrationBuilder.DropIndex(
                name: "IX_CourseOpinions_CourseId",
                table: "CourseOpinions");

            migrationBuilder.DropColumn(
                name: "CourseId",
                table: "CourseOpinions");
        }
    }
}
