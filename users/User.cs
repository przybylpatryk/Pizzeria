using System;
using System.IO;
namespace Pizzeria.users
{
    public class User
    {
        public enum Role
        {
            Client,
            Worker,
            Admin,
        }

        public string Username { get; set; }
        public string Password { get; set; }
        public Role UserRole { get; set; }

        public User(string username, string password, Role role)
        {
            Username = username;
            Password = password;
            UserRole = role;
        }
    }
}