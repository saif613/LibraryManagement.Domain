using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Application.Interfaces.ServiceInterfaces
{
    public interface ICurrentUserService
    {
        int UserId { get; }
    }
}
