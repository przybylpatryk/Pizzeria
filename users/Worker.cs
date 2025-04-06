using System;
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

        public void OrderPreview()
        {

            Console.WriteLine($"Zamówienie nr {0}");
            Console.WriteLine($"Pizza: {0}");
            Console.WriteLine($"Data zamówienia: {0}");

        }

        public void TakeOrder()
        {
            Console.Clear();
            Console.WriteLine("///////////////////////////////////////////");
            Console.WriteLine("         Podejmij się zamówienia!      ");
            Console.WriteLine("///////////////////////////////////////////");
            Console.WriteLine();
            Console.Write("Podaj numer zamówienia: ");

            string? orderNumber = Console.ReadLine();
            while (string.IsNullOrEmpty(orderNumber))
            {
                Console.Write("Pusto!, spróbuj ponownie: ");
                orderNumber = Console.ReadLine();
            }

            // tutaj dodaj kod do podejmowania zamówienia
        }
    }
}