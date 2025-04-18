using System;
using Pizzeria.users;
using Pizzeria.menus;
using Pizzeria.interfaces;
using Pizzeria.database;

namespace Pizzeria.rbac
{
    public class RBAC
    {
        public bool CanAccess(User user, string resource)
        {
            switch (resource)
            {
                case "AdminPanel":
                    return user.UserRole == User.Role.Admin;
                case "WorkerPanel":
                    return user.UserRole == User.Role.Worker;
                case "ClientPanel":
                    return user.UserRole == User.Role.Client;
                default:
                    return false;
            }
        }
    }
}
