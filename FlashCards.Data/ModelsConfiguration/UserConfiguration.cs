using FlashCards.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FlashCards.Data.ModelsConfiguration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Username)
                .HasColumnType("NVARCHAR")
                .HasMaxLength(64)
                .IsRequired();

            builder.Property(x => x.Email)
                .HasColumnType("NVARCHAR")
                .HasMaxLength(128)
                .IsRequired();

            builder.Property(x => x.Password)
                .HasColumnType("NVARCHAR")
                .HasMaxLength(256)
                .IsRequired();
        }
    }
}
