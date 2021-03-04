using FlashCards.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FlashCards.Data.ModelsConfiguration
{
    public class LessonConfiguration : IEntityTypeConfiguration<Lesson>
    {
        public void Configure(EntityTypeBuilder<Lesson> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .HasMaxLength(64)
                .IsFixedLength()
                .IsRequired();

            builder.Property(x => x.Description)
                .HasMaxLength(1024)
                .IsFixedLength();

            builder.Property(x => x.Category)
                .HasMaxLength(64)
                .IsFixedLength()
                .IsRequired();

            builder.Property(x => x.DateCreated)
                .HasColumnType("DATETIME2")
                .IsRequired();

            builder.Property(x => x.DateModified)
                .HasColumnType("DATETIME2")
                .IsRequired();

            builder.HasMany(x => x.Flashcards)
                .WithOne(x => x.Lesson);
        }
    }
}
