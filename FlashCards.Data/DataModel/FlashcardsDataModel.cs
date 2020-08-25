using FlashCards.Data.Models;
using FlashCards.Data.ModelsConfiguration;
using Microsoft.EntityFrameworkCore;

namespace FlashCards.Data.DataModel
{
    public class FlashcardsDataModel : DbContext
    {
        public FlashcardsDataModel(DbContextOptions<FlashcardsDataModel> options) : base(options) { }

        public DbSet<UserInfo> UserInfos { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Course> Courses { get; set; }

        public DbSet<CourseInfo> CourseInfos { get; set; }

        public DbSet<CourseOpinion> CourseOpinions { get; set; }

        public DbSet<Lesson> Lessons { get; set; }

        public DbSet<Flashcard> Flashcards { get; set; }

        public DbSet<SubscribedCourse> SubscribedCourses { get; set; }

        public DbSet<SubscribedLesson> SubscribedLessons { get; set; }

        public DbSet<SubscribedFlashcards> SubscribedFlashcards { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserInfoConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new CourseConfiguration());
            modelBuilder.ApplyConfiguration(new CourseInfoConfiguration());
            modelBuilder.ApplyConfiguration(new CourseOpinionConfiguration());
            modelBuilder.ApplyConfiguration(new LessonConfiguration());
            modelBuilder.ApplyConfiguration(new FlashcardConfiguration());
            modelBuilder.ApplyConfiguration(new SubscribedCourseConfiguration());
            modelBuilder.ApplyConfiguration(new SubscribedLessonConfiguration());
            modelBuilder.ApplyConfiguration(new SubscribedFlashcardCofiguration());
        }
    }
}
