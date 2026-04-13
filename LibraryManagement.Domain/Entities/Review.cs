using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManagement.Domain.Entities;

public class Review : BaseEntity
{
    public int UserId { get; private set; }
    public User? User { get; private set; }
    public int BookId { get; private set; }
    public Book? Book { get; private set; }
    public int Rating { get; private set; }
    public string? Comment { get; private set; }
    public Review(int userId, int bookId, string url, int rating, string? comment = null) => (UserId, BookId,Rating, Comment) = (userId, bookId, rating, comment);
    private Review() { }
    public void SetUser(int userId)
    {
        if (userId <= 0) throw new ArgumentException("Invalid User ID");
        UserId = userId;
    }
}

