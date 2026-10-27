using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PosEnterprise.Core.Entities;

namespace PosEnterprise.Infrastructure.Data.Configurations;

public class BlogPostConfiguration : IEntityTypeConfiguration<BlogPost>
{
    public void Configure(EntityTypeBuilder<BlogPost> builder)
    {
        builder.ToTable("BlogPosts");

        builder.HasKey(b => b.Id);

        builder.Property(b => b.Title)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(b => b.Slug)
            .IsRequired()
            .HasMaxLength(200);

        builder.HasIndex(b => b.Slug)
            .IsUnique();

        builder.Property(b => b.Summary)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(b => b.Content)
            .IsRequired();

        builder.Property(b => b.Author)
            .IsRequired()
            .HasMaxLength(100)
            .HasDefaultValue("POS Enterprise");

        builder.Property(b => b.MetaTitle)
            .HasMaxLength(200);

        builder.Property(b => b.MetaDescription)
            .HasMaxLength(300);

        builder.Property(b => b.PublishedAt)
            .HasDefaultValueSql("GETUTCDATE()");
    }
}