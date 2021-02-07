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
                .HasMaxLength(64)
                .IsFixedLength();

            builder.Property(x => x.LastName)
                .HasMaxLength(64)
                .IsFixedLength();

            builder.Property(x => x.DisplayName)
                .HasMaxLength(32)
                .IsFixedLength();

            builder.Property(x => x.City)
                .HasMaxLength(64)
                .IsFixedLength();

            builder.Property(x => x.Country)
                .HasMaxLength(64)
                .IsFixedLength();

            builder.Property(x => x.NumberOfWordsInLearningSession)
                .HasColumnType("SMALLINT")
                .HasDefaultValue(10);

            builder.Property(x => x.NumberOfWordsInReviewSession)
                .HasColumnType("SMALLINT")
                .HasDefaultValue(10);

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
