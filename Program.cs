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
            //List<Order> orders = DB.GetOrderPreview(1);
            //foreach (Order order in orders)
            //{
            //    Console.WriteLine($"ID: {order.ID}, Rodzaj Pizzy: {order.RodzajPizzy}, Data zamówienia: {order.DataZamowienia}");
            //}
            LoggingMenu LM = new LoggingMenu();
            LM.Menu();
    }
    }
}
