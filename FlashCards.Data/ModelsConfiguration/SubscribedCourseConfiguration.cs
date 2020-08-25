using FlashCards.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FlashCards.Data.ModelsConfiguration
{
    public class SubscribedCourseConfiguration : IEntityTypeConfiguration<SubscribedCourse>
    {
        public void Configure(EntityTypeBuilder<SubscribedCourse> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.LastTrainedDate)
                .HasColumnType("DATETIME2");

            builder.Property(x => x.OverallProgress)
                .HasColumnType("DECIMAL");

            builder.Property(x => x.CourseId)
                .HasColumnType("INT");

            builder.HasMany(x => x.Lessons)
                .WithOne(x => x.SubscribedCourse)
                .HasForeignKey(x => x.SubscribedCourseId);
        }
    }
}
