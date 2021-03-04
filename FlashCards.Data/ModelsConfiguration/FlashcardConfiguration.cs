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
                .HasMaxLength(64)
                .IsFixedLength()
                .IsRequired();

            builder.Property(x => x.PhrasePronunciation)
                .HasMaxLength(64)
                .IsFixedLength();

            builder.Property(x => x.PhraseSampleSentence)
                .HasMaxLength(128)
                .IsFixedLength();

            builder.Property(x => x.PhraseComment)
                .HasMaxLength(128)
                .IsFixedLength();

            builder.Property(x => x.TranslatedPhrase)
                .HasMaxLength(64)
                .IsFixedLength()
                .IsRequired();

            builder.Property(x => x.TranslatedPhraseSampleSentence)
                .HasMaxLength(128)
                .IsFixedLength();

            builder.Property(x => x.TranslatedPhraseComment)
                .HasMaxLength(128)
                .IsFixedLength();

            builder.Property(x => x.LanguageLevel)
                .HasColumnType("TINYINT")
                .HasDefaultValue(Enums.LanguageLevelEnum.NotSpecified);

            builder.Property(x => x.Category)
                .HasMaxLength(64)
                .IsFixedLength();

            builder.Property(x => x.DateCreated)
                .HasColumnType("DATETIME2")
                .IsRequired();

            builder.Property(x => x.DateModified)
                .HasColumnType("DATETIME2")
                .IsRequired();
        }
    }
}
