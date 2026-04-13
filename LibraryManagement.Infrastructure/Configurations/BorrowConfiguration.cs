using LibraryManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManagement.Infrastructure.Configurations;

public class BorrowConfiguration : IEntityTypeConfiguration<Borrow>
{
    public void Configure(EntityTypeBuilder<Borrow> builder)
    {
        builder.HasKey(b => b.Id);

        builder.Property(b => b.BorrowDate)
            .IsRequired();

        builder.Property(u => u.IsDeleted).HasDefaultValue(false);

        builder.HasQueryFilter(u => !u.IsDeleted);

        builder.Property(b => b.DueDate)
    .IsRequired();

        builder.Property(b => b.Status)
            .HasConversion<string>() // يحفظ enum كـ string
            .HasDefaultValue(BorrowStatus.Borrowed)
            .IsRequired();

        builder.HasOne(b => b.User)
            .WithMany(u => u.Borrows)
            .HasForeignKey(b => b.UserId);

        builder.HasOne(b => b.Book)
            .WithMany(book => book.borrows)
            .HasForeignKey(b => b.BookId);
    }
}

