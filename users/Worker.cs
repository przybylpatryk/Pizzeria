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
        public string Salary { get; set; }
        public WorkerMenu workerMenu { get; set; }
        public Order ActiveOrder { get; set; }
        public Database DB { get; set; }

        //konstruktor klasy Worker
        public Worker(string username, string password) : base(username, password, User.Role.Worker)
        {
            workerMenu = new WorkerMenu();
            DB = new Database();
        }

        //metoda służąca do przeglądania zamówienia
        public void OrderPreview()
        {
            DB.GetOrders();
            Console.WriteLine("\nKliknij cokolwiek by kontynuować");
            Console.ReadKey();
            workerMenu.Menu(this);
        }

        //metoda służąca do przyjmowania zamówienia
        public void TakeOrder()
        {
            Console.Clear();
            Console.WriteLine("///////////////////////////////////////////");
            Console.WriteLine("         Podejmij się zamówienia!      ");
            Console.WriteLine("///////////////////////////////////////////");
            Console.WriteLine("         Wpisz 'esc' by anulować         ");
            Console.WriteLine("///////////////////////////////////////////");
            Console.WriteLine();

            List<string> orderID = DB.GetOrderIDForW(this);
            //sprawdza czy są dostępne zamówienia
            if (orderID.Count == 0)
            {
                Console.WriteLine("Brak zamówień do podjęcia!");
                Thread.Sleep(1500);
                workerMenu.Menu(this);
                return;
            }
            DB.GetOrdersForW(this);
            Console.WriteLine();
            Console.WriteLine("Podaj ID zamówienia, które chcesz podjąć:");

            string? orderW = Console.ReadLine();
            while (true)
            {
                //sprawdza poprawność ID, które podał użytkownik
                if (!orderID.Contains(orderW))
                {
                    Console.WriteLine("Niepoprawne ID zamówienia.");
                    Thread.Sleep(1500);
                    workerMenu.Menu(this);
                    return;
                }

                //sprawdza czy użytkownik wpisał 'esc' i wtedy anuluje akcję
                if (!string.IsNullOrEmpty(orderW) && orderW.ToLower() == "esc")
                {
                    Console.WriteLine("Anulowano!");
                    Thread.Sleep(1500);
                    workerMenu.Menu(this);
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
            workerMenu.Menu(this);


        }
        //metoda służąca do realizacji zamówienia
        public void MakePizza()
        {
            List<string> orderByW = DB.GetOrderByW();
            if (orderByW.Contains(Username))
            {
                Console.Clear();
                Console.WriteLine("///////////////////////////////////////////");
                Console.WriteLine("         Stwórz pizzę!      ");
                Console.WriteLine("///////////////////////////////////////////");
                Console.WriteLine();
                Console.WriteLine("Na pewno? (Y/N)");

                string? choice = Console.ReadLine().ToLower();
                //sprawdza czy użytkownik podał poprawną wartość
                while (choice != "y" && choice != "n")
                {
                    Console.Write("Źle, Y lub N: ");
                    choice = Console.ReadLine();
                }
                //w przypadku 'y' rozpoczyna proces tworzenia pizzy
                if (choice == "y")
                {
                    Console.Write("\nZaczynasz robić placek");
                    Thread.Sleep(500);
                    for (int i = 0; i < 5; i++)
                    {
                        Console.Write(".");
                        Thread.Sleep(500);
                    }
                    Console.Write("\nDodajesz sos");
                    for (int i = 0; i < 5; i++)
                    {
                        Console.Write(".");
                        Thread.Sleep(500);
                    }
                    Console.Write("\nDodajesz ser");
                    for (int i = 0; i < 5; i++)
                    {
                        Console.Write(".");
                        Thread.Sleep(500);
                    }
                    Console.Write("\nDodajesz dodatki");
                    for (int i = 0; i < 5; i++)
                    {
                        Console.Write(".");
                        Thread.Sleep(500);
                    }
                    Console.Write("\nPieczesz");
                    for (int i = 0; i < 5; i++)
                    {
                        Console.Write(".");
                        Thread.Sleep(500);
                    }
                    Console.Write("\nPizza gotowa!");
                    DB.CompleteOrder(this);
                    Console.WriteLine("\nStworzono pizzę!");
                    Thread.Sleep(1500);
                    workerMenu.Menu(this);
                }
                //w przypadku 'n' anuluje proces tworzenia pizzy
                else if (choice == "n")
                {
                    Console.WriteLine("Anulowano tworzenie pizzy!");
                    Thread.Sleep(1500);
                    workerMenu.Menu(this);
                }
            }
            else
            {
                Console.WriteLine("Nie masz zamówienia do zrealizowania!");
                Thread.Sleep(1500);
                workerMenu.Menu(this);
            }
        }
    }
}