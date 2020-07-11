using FlashCards.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FlashCards.Data.ModelsConfiguration
{
    public class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.FirstName)
                .HasColumnType("NVARCHAR")
                .HasMaxLength(64);

            builder.Property(x => x.LastName)
                .HasColumnType("NVARCHAR")
                .HasMaxLength(64);

            builder.Property(x => x.DisplayName)
                .HasColumnType("NVARCHAR")
                .HasMaxLength(32);
        }
    }
}
