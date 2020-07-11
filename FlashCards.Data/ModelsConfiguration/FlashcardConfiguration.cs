using FlashCards.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FlashCards.Data.ModelsConfiguration
{
    public class FlashcardConfiguration : IEntityTypeConfiguration<Flashcard>
    {
        public void Configure(EntityTypeBuilder<Flashcard> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Phrase)
                .HasColumnType("NVARCHAR")
                .HasMaxLength(64)
                .IsRequired();

            builder.Property(x => x.PhrasePronunciation)
                .HasColumnType("NVARCHAR")
                .HasMaxLength(64);

            builder.Property(x => x.PhraseSampleSentence)
                .HasColumnType("NVARCHAR")
                .HasMaxLength(128);

            builder.Property(x => x.PhraseComment)
                .HasColumnType("NVARCHAR")
                .HasMaxLength(128);

            builder.Property(x => x.TranslatedPhrase)
                .HasColumnType("NVARCHAR")
                .HasMaxLength(64)
                .IsRequired();

            builder.Property(x => x.TranslatedPhraseSampleSentence)
                .HasColumnType("NVARCHAR")
                .HasMaxLength(128);

            builder.Property(x => x.LanguageLevel)
                .HasColumnType("NVARCHAR")
                .HasMaxLength(4);

            builder.Property(x => x.Category)
                .HasColumnType("NVARCHAR")
                .HasMaxLength(64);
        }
    }
}
