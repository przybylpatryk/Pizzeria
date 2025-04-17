using System;
using System.IO;
using Pizzeria.interfaces;
using Pizzeria.menus;
using Pizzeria.order;
using Pizzeria.database;

namespace Pizzeria.users
{
    public class Worker : User, IOrderManagment
    {
        public WorkerMenu workerMenu { get; set; }
        public Order ActiveOrder { get; set; }
        public Database DB { get; set; }

        public Worker(string username, string password) : base(username, password, User.Role.Worker)
        {
            workerMenu = new WorkerMenu();
            DB = new Database();
        }

        public void OrderPreview()
        {
            DB.GetOrders();
            Console.WriteLine("\nKliknij cokolwiek by kontynuować");
            Console.ReadKey();
            workerMenu.Menu(this);
        }

        public void TakeOrder()
        {
            Console.Clear();
            Console.WriteLine("///////////////////////////////////////////");
            Console.WriteLine("         Podejmij się zamówienia!      ");
            Console.WriteLine("///////////////////////////////////////////");
            Console.WriteLine("         Wpisz 'esc' by anulować         ");
            Console.WriteLine("///////////////////////////////////////////");
            Console.WriteLine();

            List<string> orderID = DB.GetOrderID();
            if (orderID.Count == 0)
            {
                Console.WriteLine("Brak zamówień do podjęcia!");
                Thread.Sleep(1500);
                this.workerMenu.Menu(this);
                return;
            }
            DB.GetOrdersForW(this);
            Console.WriteLine();
            Console.WriteLine("Podaj ID zamówienia, które chcesz podjąć:");

            string? orderW = Console.ReadLine();
            while (true)
            {
                if(orderID.Contains(orderW))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Niepoprawne ID zamówienia.");
                    Thread.Sleep(1500);
                    this.workerMenu.Menu(this);
                    return;
                }
                
                if (!string.IsNullOrEmpty(orderW) && orderW.ToLower() == "esc")
                {
                    Console.WriteLine("Anulowano dodawanie recenzji!");
                    Thread.Sleep(1500);
                    this.workerMenu.Menu(this);
                    return;
                }
                else if (!string.IsNullOrEmpty(orderW))
                {
                    break;
                }
                Console.Write("Pusto!, spróbuj ponownie: ");
                orderW = Console.ReadLine();
            }
            Console.WriteLine();
            DB.AddWOrder(orderW, this);
            Console.WriteLine($"Zamówienie o ID {orderW} zostało podjęte!");
            Thread.Sleep(1500);
            this.workerMenu.Menu(this);


        }
        public void MakePizza()
        {
            
        }
    }
}