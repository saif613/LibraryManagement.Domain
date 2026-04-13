using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManagement.Domain.Entities;

public class Borrow : BaseEntity
{
    public int UserId { get; private set; }
    public User? User { get; private set; }

    public int BookId { get; private set; }
    public Book? Book { get; private set; }
    public DateTime BorrowDate { get; private set; }

    public DateTime DueDate { get; private set; }
    public DateTime? ReturnDate { get; private set; }
    public BorrowStatus Status { get; private set; }

    public Borrow(int userId, int bookId) =>
        (UserId, BookId, BorrowDate, DueDate, Status) = (userId, bookId, DateTime.UtcNow, DateTime.UtcNow.AddDays(14), BorrowStatus.Borrowed);
    private Borrow() { }
    public void MarkAsReturned()
    {
        ReturnDate = DateTime.UtcNow;
        Status = BorrowStatus.Returned;
    }

    public void MarkAsOverdue()
    {
        Status = BorrowStatus.Overdue;
    }
    public void MarkBorrowed()
    {
        BorrowDate = DateTime.UtcNow;
        DueDate = DateTime.UtcNow.AddDays(14);
        Status = BorrowStatus.Borrowed;
    }
    public void RenewBorrow(int extraDays)
    {
        if (Status == BorrowStatus.Returned)
            throw new InvalidOperationException("Cannot renew a returned book.");

        if (extraDays <= 0)
            throw new ArgumentException("Extra days must be greater than zero.");

        DueDate = DueDate.AddDays(extraDays);

        if (Status == BorrowStatus.Overdue && DueDate > DateTime.UtcNow)
        {
            Status = BorrowStatus.Borrowed;
        }
    }
}

