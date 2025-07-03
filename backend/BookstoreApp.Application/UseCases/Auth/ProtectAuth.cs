using BookstoreApp.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BookstoreApp.Application.UseCases.Auth
{
    public class ProtectAuth : IProtectAuth
    {
        public string generateSalth()
        {
            var bytes = new byte[16];
            RandomNumberGenerator.Fill(bytes);
            return Convert.ToBase64String(bytes);
        }

        public string hashPassword(string password, string salt)
        {
            using var sha = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(password + salt);
            var hash = sha.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }
    }
}
