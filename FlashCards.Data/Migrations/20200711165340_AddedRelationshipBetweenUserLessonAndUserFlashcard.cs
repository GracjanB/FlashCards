using Microsoft.EntityFrameworkCore.Migrations;

namespace FlashCards.Data.Migrations
{
    public partial class AddedRelationshipBetweenUserLessonAndUserFlashcard : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserLessonId",
                table: "UserFlashcards",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_UserFlashcards_UserLessonId",
                table: "UserFlashcards",
                column: "UserLessonId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserFlashcards_UserLessons_UserLessonId",
                table: "UserFlashcards",
                column: "UserLessonId",
                principalTable: "UserLessons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserFlashcards_UserLessons_UserLessonId",
                table: "UserFlashcards");

            migrationBuilder.DropIndex(
                name: "IX_UserFlashcards_UserLessonId",
                table: "UserFlashcards");

            migrationBuilder.DropColumn(
                name: "UserLessonId",
                table: "UserFlashcards");
        }
    }
}
