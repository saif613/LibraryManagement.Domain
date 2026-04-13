using LibraryManagement.Domain.ValueObject;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManagement.Domain.Entities;

public class User : IdentityUser<int>, IBaseEntity
{
    public bool IsDeleted { get; set; } = false;
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; private set; }
    public string Name { get; private set; } = null!;

    public ICollection<Borrow> Borrows { get; private set; } = new List<Borrow>();
    public ICollection<Review> Reviews { get; private set; } = new List<Review>();

    public User(string name, string email, string passwordHash)
    {
        Name = name;
        Email = email;
        UserName = email;
        PasswordHash = passwordHash;
    }
    private User() { }

    public void SetUpdated() => UpdatedAt = DateTime.UtcNow;
}
