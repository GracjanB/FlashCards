using FlashCards.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FlashCards.Data.ModelsConfiguration
{
    public class CourseOpinionConfiguration : IEntityTypeConfiguration<CourseOpinion>
    {
        public void Configure(EntityTypeBuilder<CourseOpinion> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Description)
                .HasColumnType("NTEXT")
                .HasMaxLength(4000);

            builder.Property(x => x.Rating)
                .HasColumnType("TINYINT")
                .IsRequired();

            builder.Property(x => x.DateCreated)
                .HasColumnType("DATETIME2")
                .IsRequired();

            builder.Property(x => x.DateModified)
                .HasColumnType("DATETIME2")
                .IsRequired();
        }
    }
}
