using FlashCards.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FlashCards.Data.ModelsConfiguration
{
    public class UserLessonConfiguration : IEntityTypeConfiguration<UserLesson>
    {
        public void Configure(EntityTypeBuilder<UserLesson> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.ProgressPercentage)
                .HasColumnType("TINYINT");

            builder.Property(x => x.LastTrainingDate)
                .HasColumnType("DATETIME2");
        }
    }
}
