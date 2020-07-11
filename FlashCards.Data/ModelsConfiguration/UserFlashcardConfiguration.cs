using FlashCards.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FlashCards.Data.ModelsConfiguration
{
    public class UserFlashcardConfiguration : IEntityTypeConfiguration<UserFlashcard>
    {
        public void Configure(EntityTypeBuilder<UserFlashcard> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.LastTrainingDate)
                .HasColumnType("DATETIME2");

            builder.Property(x => x.LearningRate)
                .HasColumnType("TINYINT");

            builder.Property(x => x.IsLearned)
                .HasColumnType("BIT");

            builder.Property(x => x.MarkedAsHard)
                .HasColumnType("BIT");
        }
    }
}
