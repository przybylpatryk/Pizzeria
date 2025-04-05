using System;
using Pizzeria.menus;
using Pizzeria.users;
using Pizzeria.database;

namespace Pizzeria
{
    public class Program
    {
        static void Main(string[] args)
        {
            Database DB = new Database();
            LoggingMenu LM = new LoggingMenu();
            LM.Menu();
    }
    }
}
