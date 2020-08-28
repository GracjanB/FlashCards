using FlashCards.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FlashCards.Data.ModelsConfiguration
{
    public class UserInfoConfiguration : IEntityTypeConfiguration<UserInfo>
    {
        public void Configure(EntityTypeBuilder<UserInfo> builder)
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

            builder.Property(x => x.City)
                .HasColumnType("NVARCHAR(64)");

            builder.Property(x => x.Country)
                .HasColumnType("NVARCHAR(64)");

            builder.HasOne(x => x.User)
                .WithOne(x => x.UserInfo);

            builder.HasMany(x => x.CreatedCourses)
                .WithOne(x => x.AccountCreated);

            builder.HasMany(x => x.SubscribedCourses)
                .WithOne(x => x.Account)
                .HasForeignKey(x => x.AccountId);
        }
    }
}
