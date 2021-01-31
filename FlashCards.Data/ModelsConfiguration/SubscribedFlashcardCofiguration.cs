using FlashCards.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FlashCards.Data.ModelsConfiguration
{
    public class SubscribedFlashcardCofiguration : IEntityTypeConfiguration<SubscribedFlashcards>
    {
        public void Configure(EntityTypeBuilder<SubscribedFlashcards> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.LastTrainingDate)
                .HasColumnType("DATETIME2");

            builder.Property(x => x.LastRevisionDate)
                .HasColumnType("DATETIME2");

            builder.Property(x => x.TrainLevel)
                .HasColumnType("TINYINT");

            builder.Property(x => x.MarkedAsHard)
                .HasColumnType("BIT");

            builder.Property(x => x.MarkedAsIgnored)
                .HasColumnType("BIT")
                .HasDefaultValueSql("0");

            builder.Property(x => x.DifficultyLevel)
                .HasColumnType("TINYINT");

            builder.Property(x => x.FlashcardId)
                .HasColumnType("INT");
        }
    }
}
