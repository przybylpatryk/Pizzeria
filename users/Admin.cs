using System;
using System.IO;
using Pizzeria.interfaces;
using Pizzeria.menus;

namespace Pizzeria.users
{
    public class Admin : User, IOrderManagment, IWorkerManagment
    {
        public AdminMenu adminMenu { get; set; }

        public Admin(string username, string password) : base(username, password, User.Role.Admin)
        {
            adminMenu = new AdminMenu();
        }
    }
}