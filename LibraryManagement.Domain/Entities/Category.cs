using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManagement.Domain.Entities;

public class Category : BaseEntity
{
    public string Name { get; private set; } = null!;
    public string? Description { get; private set; }
    public ICollection<Book> Books { get; private set; } = new List<Book>();

    public Category(string name, string? description = null) => (Name, Description) = (name, description);
    private Category() { }
}

