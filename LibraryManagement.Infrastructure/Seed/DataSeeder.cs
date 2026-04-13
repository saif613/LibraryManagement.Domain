using Bogus;
using LibraryManagement.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

public static class DataSeeder
{
    public static void Seed(ModelBuilder modelBuilder)
    {
        // 1. تثبيت البذرة العشوائية لـ Bogus لضمان توليد نفس البيانات دائماً
        Randomizer.Seed = new Random(8675309);
        var staticDate = new DateTime(2025, 12, 1, 0, 0, 0, DateTimeKind.Utc);

        // 2. هاش ثابت لكلمة السر "P@ssword123"
        const string staticHash = "AQAAAAIAAYagAAAAEO9v9p6X3GjS5I5f6m4g5r9k5l5t5y5u5i5o5p5";

        // --- أ. توليد بيانات الأقسام (Categories) ---
        var categoryId = 1;
        var categories = new Faker<Category>()
            .CustomInstantiator(f => new Category(f.Commerce.Categories(1)[0], f.Lorem.Sentence()))
            .RuleFor(c => c.Id, f => categoryId++)
            .Generate(5);

        modelBuilder.Entity<Category>().HasData(categories.Select(c => new {
            c.Id,
            c.Name,
            c.Description,
            c.IsDeleted,
            CreatedAt = staticDate
        }));

        // --- ب. إعداد الأدوار (Roles) ---
        modelBuilder.Entity<IdentityRole<int>>().HasData(
            new IdentityRole<int> { Id = 1, Name = "Admin", NormalizedName = "ADMIN", ConcurrencyStamp = "ROLE_STAMP_1" },
            new IdentityRole<int> { Id = 2, Name = "Member", NormalizedName = "MEMBER", ConcurrencyStamp = "ROLE_STAMP_2" }
        );

        // --- ج. توليد بيانات المستخدمين (Users) ---
        var userIdCounter = 1;
        var users = new Faker<User>()
            .CustomInstantiator(f => new User(f.Name.FullName(), f.Internet.Email(), ""))
            .Generate(10);

        var seededUsers = users.Select(u => {
            var currentId = userIdCounter++;
            return new
            {
                Id = currentId,
                u.Name,
                u.Email,
                NormalizedEmail = u.Email!.ToUpper(),
                UserName = u.Email,
                NormalizedUserName = u.Email!.ToUpper(),
                PasswordHash = staticHash,
                SecurityStamp = "USER_STAMP_" + currentId,
                ConcurrencyStamp = "USER_CONCUR_" + currentId,
                u.IsDeleted,
                CreatedAt = staticDate,
                EmailConfirmed = true,
                PhoneNumberConfirmed = false,
                TwoFactorEnabled = false,
                LockoutEnabled = true,
                AccessFailedCount = 0
            };
        }).ToList();

        modelBuilder.Entity<User>().HasData(seededUsers);

        // --- د. ربط المستخدمين بالأدوار (UserRoles) ---
        modelBuilder.Entity<IdentityUserRole<int>>().HasData(
            seededUsers.Select(u => new IdentityUserRole<int> { UserId = u.Id, RoleId = u.Id == 1 ? 1 : 2 })
        );

        // --- هـ. توليد بيانات الكتب (Books) ---
        var bookIdCounter = 1;
        var books = new Faker<Book>()
            .CustomInstantiator(f => new Book(
                f.Commerce.ProductName(),
                f.Name.FullName(),
                f.Commerce.Ean13(),
                f.Image.PicsumUrl(),
                f.Random.Int(1, 50),
                f.PickRandom(categories).Id
            ))
            .RuleFor(b => b.Id, f => bookIdCounter++)
            .Generate(30);

        modelBuilder.Entity<Book>().HasData(books.Select(b => new {
            b.Id,
            b.Title,
            b.Author,
            b.ISBN,
            b.URL,
            b.StockQuantity,
            b.CategoryId,
            b.IsDeleted,
            CreatedAt = staticDate
        }));

        // --- و. توليد بيانات الاستعارات (Borrows) مع الـ DueDate الجديد ---
        var borrowIdCounter = 1;
        var borrows = new Faker<Borrow>()
            .CustomInstantiator(f => new Borrow(
                f.PickRandom(seededUsers).Id,
                f.PickRandom(books).Id
            ))
            .RuleFor(b => b.Id, f => borrowIdCounter++)
            .Generate(15);

        modelBuilder.Entity<Borrow>().HasData(borrows.Select(b => new {
            b.Id,
            b.UserId,
            b.BookId,
            BorrowDate = staticDate,
            DueDate = staticDate.AddDays(14), // تحديد تاريخ الاستحقاق يدوياً في الـ Seeding
            Status = b.Status,
            b.IsDeleted,
            CreatedAt = staticDate
        }));

        // --- ز. توليد بيانات التقييمات (Reviews) ---
        var reviewIdCounter = 1;
        var reviews = new Faker<Review>()
            .CustomInstantiator(f => new Review(
                f.PickRandom(seededUsers).Id,
                f.PickRandom(books).Id,
                f.Image.PicsumUrl(),
                f.Random.Int(1, 5),
                f.Lorem.Sentence()
            ))
            .RuleFor(r => r.Id, f => reviewIdCounter++)
            .Generate(20);

        modelBuilder.Entity<Review>().HasData(reviews.Select(r => new {
            r.Id,
            r.UserId,
            r.BookId,
            r.Rating,
            r.Comment,
            r.IsDeleted,
            CreatedAt = staticDate
        }));
    }
}