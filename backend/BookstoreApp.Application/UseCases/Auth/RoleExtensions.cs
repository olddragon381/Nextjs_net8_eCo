using BookstoreApp.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookstoreApp.Application.UseCases.Auth
{
    public static class RoleExtensions
    {
        public static bool CanCreateRole(this Role currentUserRole, Role newRole)
        {
            return currentUserRole switch
            {
                Role.SuperAdmin => true, // ✅ Có thể tạo bất kỳ role nào
                Role.Admin => newRole == Role.User, // ✅ Admin chỉ được tạo User
                Role.User => newRole == Role.User, 
                _ => false
            };
        }
        public static bool CanUpdateRole(this Role currentUserRole, Role newRole)
        {
            if (newRole == Role.SuperAdmin)
            {
                return currentUserRole == Role.SuperAdmin;
            }
            return true;
        }
    }
}
