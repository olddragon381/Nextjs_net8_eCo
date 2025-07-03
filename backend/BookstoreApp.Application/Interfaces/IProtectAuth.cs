using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookstoreApp.Application.Interfaces
{
    public interface IProtectAuth
    {
        string generateSalth();
        string hashPassword(string password, string salt);
    }
}
