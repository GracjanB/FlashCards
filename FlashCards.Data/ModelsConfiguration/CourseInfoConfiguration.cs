using FlashCards.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FlashCards.Data.ModelsConfiguration
{
    public class CourseInfoConfiguration : IEntityTypeConfiguration<CourseInfo>
    {
        public void Configure(EntityTypeBuilder<CourseInfo> builder)
        {
            builder.HasKey(x => x.Id);
        }
    }
}
