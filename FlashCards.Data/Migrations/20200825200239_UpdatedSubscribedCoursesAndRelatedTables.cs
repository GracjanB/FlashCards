using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FlashCards.Data.Migrations
{
    public partial class UpdatedSubscribedCoursesAndRelatedTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserFlashcards");

            migrationBuilder.DropTable(
                name: "UserLessons");

            migrationBuilder.DropTable(
                name: "UserCourses");

            migrationBuilder.CreateTable(
                name: "SubscribedCourses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LastTrainedDate = table.Column<DateTime>(type: "DATETIME2", nullable: false),
                    OverallProgress = table.Column<decimal>(type: "DECIMAL", nullable: false),
                    AccountId = table.Column<int>(nullable: false),
                    CourseId = table.Column<int>(type: "INT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubscribedCourses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubscribedCourses_UserInfos_AccountId",
                        column: x => x.AccountId,
                        principalTable: "UserInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubscribedLessons",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OverallProgress = table.Column<decimal>(type: "DECIMAL", nullable: false),
                    LastTrainingDate = table.Column<DateTime>(type: "DATETIME2", nullable: false),
                    LessonId = table.Column<int>(type: "INT", nullable: false),
                    SubscribedCourseId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubscribedLessons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubscribedLessons_SubscribedCourses_SubscribedCourseId",
                        column: x => x.SubscribedCourseId,
                        principalTable: "SubscribedCourses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubscribedFlashcards",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LastTrainingDate = table.Column<DateTime>(type: "DATETIME2", nullable: false),
                    TrainLevel = table.Column<byte>(type: "TINYINT", nullable: false),
                    DifficultyLevel = table.Column<byte>(type: "TINYINT", nullable: false),
                    MarkedAsHard = table.Column<bool>(type: "BIT", nullable: false),
                    FlashcardId = table.Column<int>(type: "INT", nullable: false),
                    SubscribedLessonId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubscribedFlashcards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubscribedFlashcards_SubscribedLessons_SubscribedLessonId",
                        column: x => x.SubscribedLessonId,
                        principalTable: "SubscribedLessons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SubscribedCourses_AccountId",
                table: "SubscribedCourses",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_SubscribedFlashcards_SubscribedLessonId",
                table: "SubscribedFlashcards",
                column: "SubscribedLessonId");

            migrationBuilder.CreateIndex(
                name: "IX_SubscribedLessons_SubscribedCourseId",
                table: "SubscribedLessons",
                column: "SubscribedCourseId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SubscribedFlashcards");

            migrationBuilder.DropTable(
                name: "SubscribedLessons");

            migrationBuilder.DropTable(
                name: "SubscribedCourses");

            migrationBuilder.CreateTable(
                name: "UserCourses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountId = table.Column<int>(type: "int", nullable: false),
                    CourseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserCourses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserCourses_UserInfos_AccountId",
                        column: x => x.AccountId,
                        principalTable: "UserInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserCourses_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserLessons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LastTrainingDate = table.Column<DateTime>(type: "DATETIME2", nullable: false),
                    ProgressPercentage = table.Column<byte>(type: "TINYINT", nullable: false),
                    UserCourseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLessons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserLessons_UserCourses_UserCourseId",
                        column: x => x.UserCourseId,
                        principalTable: "UserCourses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserFlashcards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsLearned = table.Column<bool>(type: "BIT", nullable: false),
                    LastTrainingDate = table.Column<DateTime>(type: "DATETIME2", nullable: false),
                    LearningRate = table.Column<byte>(type: "TINYINT", nullable: false),
                    MarkedAsHard = table.Column<bool>(type: "BIT", nullable: false),
                    UserLessonId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserFlashcards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserFlashcards_UserLessons_UserLessonId",
                        column: x => x.UserLessonId,
                        principalTable: "UserLessons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserCourses_AccountId",
                table: "UserCourses",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_UserCourses_CourseId",
                table: "UserCourses",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_UserFlashcards_UserLessonId",
                table: "UserFlashcards",
                column: "UserLessonId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLessons_UserCourseId",
                table: "UserLessons",
                column: "UserCourseId");
        }
    }
}
