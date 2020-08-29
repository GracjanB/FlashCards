using FlashCards.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Security.Cryptography.X509Certificates;

namespace FlashCards.Data.ModelsConfiguration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Email)
                .HasColumnType("NVARCHAR(128)")
                .IsRequired();

            builder.Property(x => x.PasswordHash)
                .HasColumnType("BINARY(512)")
                .IsRequired();

            builder.Property(x => x.PasswordSalt)
                .HasColumnType("BINARY(512)")
                .IsRequired();
        }
    }
}
