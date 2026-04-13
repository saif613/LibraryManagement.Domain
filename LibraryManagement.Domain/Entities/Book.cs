using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManagement.Domain.Entities;

public class Book : BaseEntity
{
    public string Title { get; private set; } = null!;
    public string Author { get; private set; } = null!;
    public string ISBN { get; private set; } = null!;
    public string URL { get; private set; } = null!;
    public int StockQuantity { get; private set; }
    public int CategoryId { get; private set; }
    public Category? Category { get; private set; }
    public ICollection<Borrow> borrows { get; private set; } = new List<Borrow>();
    public ICollection<Review> Reviews { get; private set; } = new List<Review>();
    public Book(string title, string author, string isbn, string url, int stockQuantity, int categoryId) =>
        (Title, Author, ISBN, URL, StockQuantity, CategoryId) = (title, author, isbn, url, stockQuantity, categoryId);
    private Book() { }

    public void ReduceStock(int quantity)
    {
        if (quantity <= 0)
            throw new ArgumentException("Quantity must be greater than zero.");
        if (StockQuantity < quantity)
            throw new InvalidOperationException("Insufficient stock to reduce.");
        StockQuantity -= quantity;
    }

    public void IncreaseStock(int quantity)
    {
        if (quantity <= 0)
            throw new ArgumentException("Quantity must be greater than zero.");
        StockQuantity += quantity;
    }
}

