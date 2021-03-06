﻿using FlashCards.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FlashCards.Data.ModelsConfiguration
{
    public class CourseConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .HasMaxLength(64)
                .IsFixedLength()
                .IsRequired();

            builder.Property(x => x.Description)
                .HasColumnType("NTEXT")
                .HasMaxLength(4000)
                .IsFixedLength();

            builder.Property(x => x.DateCreated)
                .HasColumnType("DATETIME2")
                .IsRequired();

            builder.Property(x => x.DateModified)
                .HasColumnType("DATETIME2")
                .IsRequired();

            builder.Property(x => x.CourseType)
                .HasColumnType("TINYINT")
                .IsRequired();

            builder.Property(x => x.Status)
                .HasColumnType("TINYINT")
                .IsRequired();

            builder.HasMany(x => x.Lessons)
                .WithOne(x => x.Course);

            builder.HasOne(x => x.CourseInfo)
                .WithOne(x => x.Course);

            builder.HasMany(x => x.Opinions)
                .WithOne(x => x.Course);
        }
    }
}
