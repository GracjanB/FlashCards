﻿// <auto-generated />
using System;
using FlashCards.Data.DataModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FlashCards.Data.Migrations
{
    [DbContext(typeof(FlashcardsDataModel))]
    partial class FlashcardsDataModelModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("FlashCards.Data.Models.Course", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AccountCreatedId")
                        .HasColumnType("int");

                    b.Property<byte>("CourseType")
                        .HasColumnType("TINYINT");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("DATETIME2");

                    b.Property<DateTime>("DateModified")
                        .HasColumnType("DATETIME2");

                    b.Property<string>("Description")
                        .HasColumnType("NTEXT")
                        .HasMaxLength(4000);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("NVARCHAR")
                        .HasMaxLength(128);

                    b.HasKey("Id");

                    b.HasIndex("AccountCreatedId");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("FlashCards.Data.Models.CourseInfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AmountOfEnrolled")
                        .HasColumnType("INT");

                    b.Property<int>("CourseId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CourseId")
                        .IsUnique();

                    b.ToTable("CourseInfos");
                });

            modelBuilder.Entity("FlashCards.Data.Models.CourseOpinion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CourseId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("DATETIME2");

                    b.Property<DateTime>("DateModified")
                        .HasColumnType("DATETIME2");

                    b.Property<string>("Description")
                        .HasColumnType("NTEXT")
                        .HasMaxLength(4000);

                    b.Property<byte>("Rating")
                        .HasColumnType("TINYINT");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.ToTable("CourseOpinions");
                });

            modelBuilder.Entity("FlashCards.Data.Models.Flashcard", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Category")
                        .HasColumnType("NVARCHAR")
                        .HasMaxLength(64);

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("DATETIME2");

                    b.Property<DateTime>("DateModified")
                        .HasColumnType("DATETIME2");

                    b.Property<byte>("LanguageLevel")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TINYINT")
                        .HasDefaultValue((byte)7);

                    b.Property<int>("LessonId")
                        .HasColumnType("int");

                    b.Property<string>("Phrase")
                        .IsRequired()
                        .HasColumnType("NVARCHAR")
                        .HasMaxLength(64);

                    b.Property<string>("PhraseComment")
                        .HasColumnType("NVARCHAR")
                        .HasMaxLength(128);

                    b.Property<string>("PhrasePronunciation")
                        .HasColumnType("NVARCHAR")
                        .HasMaxLength(64);

                    b.Property<string>("PhraseSampleSentence")
                        .HasColumnType("NVARCHAR")
                        .HasMaxLength(128);

                    b.Property<string>("TranslatedPhrase")
                        .IsRequired()
                        .HasColumnType("NVARCHAR")
                        .HasMaxLength(64);

                    b.Property<string>("TranslatedPhraseComment")
                        .HasColumnType("NVARCHAR(128)");

                    b.Property<string>("TranslatedPhraseSampleSentence")
                        .HasColumnType("NVARCHAR")
                        .HasMaxLength(128);

                    b.HasKey("Id");

                    b.HasIndex("LessonId");

                    b.ToTable("Flashcards");
                });

            modelBuilder.Entity("FlashCards.Data.Models.Lesson", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasColumnType("NVARCHAR")
                        .HasMaxLength(64);

                    b.Property<int>("CourseId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("DATETIME2");

                    b.Property<DateTime>("DateModified")
                        .HasColumnType("DATETIME2");

                    b.Property<string>("Description")
                        .HasColumnType("NTEXT")
                        .HasMaxLength(1024);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("NVARCHAR(64)");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.ToTable("Lessons");
                });

            modelBuilder.Entity("FlashCards.Data.Models.SubscribedCourse", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AccountId")
                        .HasColumnType("int");

                    b.Property<int>("CourseId")
                        .HasColumnType("INT");

                    b.Property<bool>("IsSubscribed")
                        .HasColumnType("BIT");

                    b.Property<DateTime>("LastActivity")
                        .HasColumnType("DATETIME");

                    b.Property<decimal>("OverallProgress")
                        .HasColumnType("DECIMAL");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.ToTable("SubscribedCourses");
                });

            modelBuilder.Entity("FlashCards.Data.Models.SubscribedFlashcards", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<byte>("DifficultyLevel")
                        .HasColumnType("TINYINT");

                    b.Property<int>("FlashcardId")
                        .HasColumnType("INT");

                    b.Property<DateTime>("LastRevisionDate")
                        .HasColumnType("DATETIME2");

                    b.Property<DateTime>("LastTrainingDate")
                        .HasColumnType("DATETIME2");

                    b.Property<bool>("MarkedAsHard")
                        .HasColumnType("BIT");

                    b.Property<bool>("MarkedAsIgnored")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("BIT")
                        .HasDefaultValueSql("0");

                    b.Property<int>("SubscribedLessonId")
                        .HasColumnType("int");

                    b.Property<byte>("TrainLevel")
                        .HasColumnType("TINYINT");

                    b.HasKey("Id");

                    b.HasIndex("SubscribedLessonId");

                    b.ToTable("SubscribedFlashcards");
                });

            modelBuilder.Entity("FlashCards.Data.Models.SubscribedLesson", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("LastTrainingDate")
                        .HasColumnType("DATETIME2");

                    b.Property<int>("LessonId")
                        .HasColumnType("INT");

                    b.Property<decimal>("OverallProgress")
                        .HasColumnType("DECIMAL");

                    b.Property<int>("SubscribedCourseId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SubscribedCourseId");

                    b.ToTable("SubscribedLessons");
                });

            modelBuilder.Entity("FlashCards.Data.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("NVARCHAR(128)");

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("BINARY(64)");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("BINARY(128)");

                    b.Property<int>("UserInfoId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserInfoId")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("FlashCards.Data.Models.UserInfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("City")
                        .HasColumnType("NVARCHAR(64)");

                    b.Property<string>("Country")
                        .HasColumnType("NVARCHAR(64)");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<string>("DisplayName")
                        .HasColumnType("NVARCHAR(32)");

                    b.Property<string>("FirstName")
                        .HasColumnType("NVARCHAR(64)");

                    b.Property<string>("LastName")
                        .HasColumnType("NVARCHAR(64)");

                    b.Property<short>("NumberOfWordsInLearningSession")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("SMALLINT")
                        .HasDefaultValue((short)10);

                    b.Property<short>("NumberOfWordsInReviewSession")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("SMALLINT")
                        .HasDefaultValue((short)10);

                    b.Property<string>("PhotoUrl")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("UserInfos");
                });

            modelBuilder.Entity("FlashCards.Data.Models.Course", b =>
                {
                    b.HasOne("FlashCards.Data.Models.UserInfo", "AccountCreated")
                        .WithMany("CreatedCourses")
                        .HasForeignKey("AccountCreatedId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FlashCards.Data.Models.CourseInfo", b =>
                {
                    b.HasOne("FlashCards.Data.Models.Course", "Course")
                        .WithOne("CourseInfo")
                        .HasForeignKey("FlashCards.Data.Models.CourseInfo", "CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FlashCards.Data.Models.CourseOpinion", b =>
                {
                    b.HasOne("FlashCards.Data.Models.Course", "Course")
                        .WithMany("Opinions")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FlashCards.Data.Models.Flashcard", b =>
                {
                    b.HasOne("FlashCards.Data.Models.Lesson", "Lesson")
                        .WithMany("Flashcards")
                        .HasForeignKey("LessonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FlashCards.Data.Models.Lesson", b =>
                {
                    b.HasOne("FlashCards.Data.Models.Course", "Course")
                        .WithMany("Lessons")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FlashCards.Data.Models.SubscribedCourse", b =>
                {
                    b.HasOne("FlashCards.Data.Models.UserInfo", "Account")
                        .WithMany("SubscribedCourses")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FlashCards.Data.Models.SubscribedFlashcards", b =>
                {
                    b.HasOne("FlashCards.Data.Models.SubscribedLesson", "SubscribedLesson")
                        .WithMany("SubscribedFlashcards")
                        .HasForeignKey("SubscribedLessonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FlashCards.Data.Models.SubscribedLesson", b =>
                {
                    b.HasOne("FlashCards.Data.Models.SubscribedCourse", "SubscribedCourse")
                        .WithMany("Lessons")
                        .HasForeignKey("SubscribedCourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FlashCards.Data.Models.User", b =>
                {
                    b.HasOne("FlashCards.Data.Models.UserInfo", "UserInfo")
                        .WithOne("User")
                        .HasForeignKey("FlashCards.Data.Models.User", "UserInfoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
