using Microsoft.EntityFrameworkCore.Migrations;

namespace FlashCards.Data.Migrations
{
    public partial class AddedRelationshipBetweenLessonAndFlashcard : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LessonId",
                table: "Flashcards",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Flashcards_LessonId",
                table: "Flashcards",
                column: "LessonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Flashcards_Lessons_LessonId",
                table: "Flashcards",
                column: "LessonId",
                principalTable: "Lessons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Flashcards_Lessons_LessonId",
                table: "Flashcards");

            migrationBuilder.DropIndex(
                name: "IX_Flashcards_LessonId",
                table: "Flashcards");

            migrationBuilder.DropColumn(
                name: "LessonId",
                table: "Flashcards");
        }
    }
}
