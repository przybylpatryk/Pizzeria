﻿using System;
using System.IO;
using Pizzeria.interfaces;
using Pizzeria.menus;

namespace Pizzeria.users
{
    public class Worker : User, IOrderManagment
    {
        public WorkerMenu workerMenu { get; set; }

        public Worker(string username, string password) : base(username, password, User.Role.Worker)
        {
            workerMenu = new WorkerMenu();
        }
    }
}