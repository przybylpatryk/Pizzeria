using System;
using Pizzeria.users;
using Pizzeria.menus;
using Pizzeria.interfaces;
using Pizzeria.database;
using Pizzeria.menus;

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

        public bool HasPerms(User user, string resource)
        {
            switch (resource)
            {
                case "CreateOrder":
                    return user.UserRole == User.Role.Client;
                case "AddReview":
                    return user.UserRole == User.Role.Client;
                case "TakeOrder":
                    return user.UserRole == User.Role.Worker;
                case "MakePizza":
                    return user.UserRole == User.Role.Worker;
                case "ManageOrders":
                    return user.UserRole == User.Role.Admin;
                case "ManageUsers":
                    return user.UserRole == User.Role.Admin;
                case "AddPizza":
                    return user.UserRole == User.Role.Admin;
                case "DeletePizza":
                    return user.UserRole == User.Role.Admin;
                default:
                    return false;
            }
        }
    }
}
