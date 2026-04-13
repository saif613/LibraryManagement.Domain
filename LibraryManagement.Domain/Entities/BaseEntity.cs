using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManagement.Domain.Entities;

public abstract class BaseEntity: IBaseEntity
{
    public int Id { get; private set; }
    public bool IsDeleted { get; set; } = false;
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; private set; }
}

