using LibraryManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManagement.Infrastructure.Configurations;

public class BookConfiguration : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder.HasKey(b => b.Id);

        builder.Property(b => b.Title)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(b => b.Author)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasIndex(b => b.ISBN)
            .IsUnique();

        builder.Property(b => b.ISBN)
            .IsRequired()
            .HasMaxLength(20);
                   

        builder.Property(u => u.IsDeleted).HasDefaultValue(false);

        builder.HasQueryFilter(u => !u.IsDeleted);

        builder.Property(b => b.StockQuantity)
               .IsRequired();

        builder.Property(r => r.URL)
               .IsRequired()           // لازم يكون موجود
               .HasMaxLength(500);

        builder.HasMany(b => b.borrows)
               .WithOne(br => br.Book)
               .HasForeignKey(br => br.BookId);

        builder.HasMany(b => b.Reviews)
               .WithOne(r => r.Book)
               .HasForeignKey(r => r.BookId);
    }
}

