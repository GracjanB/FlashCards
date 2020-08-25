using FlashCards.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FlashCards.Data.ModelsConfiguration
{
    public class SubscribedLessonConfiguration : IEntityTypeConfiguration<SubscribedLesson>
    {
        public void Configure(EntityTypeBuilder<SubscribedLesson> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.OverallProgress)
                .HasColumnType("DECIMAL");

            builder.Property(x => x.LastTrainingDate)
                .HasColumnType("DATETIME2");

            builder.Property(x => x.LessonId)
                .HasColumnType("INT");

            builder.HasMany(x => x.SubscribedFlashcards)
                .WithOne(x => x.SubscribedLesson)
                .HasForeignKey(x => x.SubscribedLessonId);
        }
    }
}
