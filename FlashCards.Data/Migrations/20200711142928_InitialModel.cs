using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FlashCards.Data.Migrations
{
    public partial class InitialModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "NVARCHAR", maxLength: 64, nullable: true),
                    LastName = table.Column<string>(type: "NVARCHAR", maxLength: 64, nullable: true),
                    DisplayName = table.Column<string>(type: "NVARCHAR", maxLength: 32, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CourseInfos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AmountOfEnrolled = table.Column<int>(type: "INT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseInfos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CourseOpinions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "NTEXT", maxLength: 4000, nullable: true),
                    Rating = table.Column<byte>(type: "TINYINT", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "DATETIME2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "DATETIME2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseOpinions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "NVARCHAR", maxLength: 128, nullable: false),
                    Description = table.Column<string>(type: "NTEXT", maxLength: 4000, nullable: true),
                    DateCreated = table.Column<DateTime>(type: "DATETIME2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "DATETIME2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Flashcards",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Phrase = table.Column<string>(type: "NVARCHAR", maxLength: 64, nullable: false),
                    PhrasePronunciation = table.Column<string>(type: "NVARCHAR", maxLength: 64, nullable: true),
                    PhraseSampleSentence = table.Column<string>(type: "NVARCHAR", maxLength: 128, nullable: true),
                    PhraseComment = table.Column<string>(type: "NVARCHAR", maxLength: 128, nullable: true),
                    TranslatedPhrase = table.Column<string>(type: "NVARCHAR", maxLength: 64, nullable: false),
                    TranslatedPhraseSampleSentence = table.Column<string>(type: "NVARCHAR", maxLength: 128, nullable: true),
                    LanguageLevel = table.Column<string>(type: "NVARCHAR", maxLength: 4, nullable: true),
                    Category = table.Column<string>(type: "NVARCHAR", maxLength: 64, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flashcards", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Lessons",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "NTEXT", maxLength: 1024, nullable: true),
                    Category = table.Column<string>(type: "NVARCHAR", maxLength: 64, nullable: false),
                    DateCreated = table.Column<DateTime>(type: "DATETIME2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "DATETIME2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lessons", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserCourses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserCourses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserFlashcards",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LastTrainingDate = table.Column<DateTime>(type: "DATETIME2", nullable: false),
                    LearningRate = table.Column<byte>(type: "TINYINT", nullable: false),
                    IsLearned = table.Column<bool>(type: "BIT", nullable: false),
                    MarkedAsHard = table.Column<bool>(type: "BIT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserFlashcards", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserLessons",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProgressPercentage = table.Column<byte>(type: "TINYINT", nullable: false),
                    LastTrainingDate = table.Column<DateTime>(type: "DATETIME2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLessons", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "NVARCHAR", maxLength: 64, nullable: false),
                    Email = table.Column<string>(type: "NVARCHAR", maxLength: 128, nullable: false),
                    Password = table.Column<string>(type: "NVARCHAR", maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "CourseInfos");

            migrationBuilder.DropTable(
                name: "CourseOpinions");

            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropTable(
                name: "Flashcards");

            migrationBuilder.DropTable(
                name: "Lessons");

            migrationBuilder.DropTable(
                name: "UserCourses");

            migrationBuilder.DropTable(
                name: "UserFlashcards");

            migrationBuilder.DropTable(
                name: "UserLessons");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
