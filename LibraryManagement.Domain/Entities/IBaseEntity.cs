using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManagement.Domain.Entities
{
    public interface IBaseEntity
    {
        int Id { get; }
        bool IsDeleted { get; set; }
        DateTime CreatedAt { get; }
        DateTime? UpdatedAt { get; }
    }
}
