using BookstoreApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookstoreApp.Application.Interfaces
{
    public interface IJwtProvider
    {
        string GenerateToken(User user);
    }
}
