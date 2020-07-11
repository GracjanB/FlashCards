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
        }
    }
}
