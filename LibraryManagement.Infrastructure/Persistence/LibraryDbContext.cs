using LibraryManagement.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManagement.Infrastructure.Persistence;

public class LibraryDbContext : IdentityDbContext<User, IdentityRole<int>, int>
{
   public LibraryDbContext(DbContextOptions<LibraryDbContext> options)
          : base(options) { }

    public DbSet<Category> Categories { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<Borrow> Borrows { get; set; }
    public DbSet<Review> Reviews { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>().ToTable("Users");

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(LibraryDbContext).Assembly);

        DataSeeder.Seed(modelBuilder);
    }

    public override int SaveChanges()
    {
        ApplyAuditColumns();
        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        ApplyAuditColumns();
        return base.SaveChangesAsync(cancellationToken);
    }

    private void ApplyAuditColumns()
    {
        var now = DateTime.UtcNow;

        foreach (var entry in ChangeTracker.Entries().Where(e => e.State == EntityState.Added))
        {
            if (entry.Properties.FirstOrDefault(p => p.Metadata.Name == "CreatedAt") is { } created)
            {
                created.CurrentValue = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries().Where(e => e.State == EntityState.Modified))
        {
            if (entry.Properties.FirstOrDefault(p => p.Metadata.Name == "UpdatedAt") is { } updated)
            {
                updated.CurrentValue = now;
            }
        }
    }
}

