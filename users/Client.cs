using System;
using System.IO;
using Pizzeria.interfaces;
using Pizzeria.menus;
using Pizzeria.database;
using Pizzeria.order;
using Pizzeria.pizza;

namespace Pizzeria.users
{
	public class Client : User, IOrderManagment
	{
		public ClientMenu clientMenu { get; set; }
        public Database DB { get; set; }
        public Order ActiveOrder { get; set; }

        public Client(string username, string password) : base(username, password, User.Role.Client)
        {
            clientMenu = new ClientMenu();
            DB = new Database();
        }

		public void CreateOrder()
		{
            Console.Clear();
            Console.WriteLine("/////////////////////////////////////////////////////////////////////////////////////////////////////////////////");
            Console.WriteLine("                                         Wybierz jedną z pizz                                                    ");
            Console.WriteLine("/////////////////////////////////////////////////////////////////////////////////////////////////////////////////");
            Console.WriteLine("                                                                        Mała  |  Średnia  |  Duża  |  Bardzo Duża");
            Console.WriteLine("1.Margherita       |   ser, sos pomidorowy                               20         25        35           40    ");
            Console.WriteLine("2.Pepperoni        |   ser, sos pomidorowy, pepperoni                   22.5       27.5      37.5         42.5   ");
            Console.WriteLine("3.Hawajska         |   ser, sos pomidorowy, ananas, szynka              24.5       29.5      39.5         44.5   ");
            Console.WriteLine("4.Capricciosa      |   ser, sos pomidorowy, szynka, pieczarki, oliwki    25         30        40           45    ");
            Console.WriteLine("5.Johnny's Special |   ser, sos pomidorowy, kurczak, szynka, salami      25         30        40           45    ");
            Console.WriteLine();
            Console.WriteLine("6.Anuluj");
            string? choice = Console.ReadLine();
            Console.WriteLine();
            while (string.IsNullOrEmpty(choice))
            {
                Console.Write("Pusto!, spróbuj ponownie: ");
                choice = Console.ReadLine();
            }
            if(choice != "1" && choice != "2" && choice != "3" && choice != "4" && choice != "5" && choice != "6")
            {
                Console.WriteLine("Niepoprawny wybór, spróbuj ponownie.");
                Thread.Sleep(1500);
                CreateOrder();
            }
            if(choice == "6")
            {
                Console.WriteLine("Anulowano zamówienie!");
                Thread.Sleep(1500);
                clientMenu.Menu(this);
            }
            Console.WriteLine("Podaj rozmiar pizzy (Mała, Średnia, Duża, Bardzo Duża)");
            string? sizeInput = Console.ReadLine().ToLower();
            while (string.IsNullOrEmpty(sizeInput) || (sizeInput != "mała" && sizeInput != "średnia" && sizeInput != "duża" && sizeInput != "bardzo duża")) 
            {
                Console.Write("Niepoprawny rozmiar pizzy, spróbuj ponownie (Mała, Średnia, Duża, Bardzo Duża): ");
                sizeInput = Console.ReadLine().ToLower();
            }
            switch(sizeInput)
            {
                case "mała":
                    sizeInput = "Small";
                    break;
                case "średnia":
                    sizeInput = "Medium";
                    break;
                case "duża":
                    sizeInput = "Large";
                    break;
                case "bardzo duża":
                    sizeInput = "ExtraLarge";
                    break;
            }
            if(Enum.TryParse(sizeInput, out size size))
            {
                Console.WriteLine($"Wybrano rozmiar pizzy: {size}");
            }
            else
            {
                Console.WriteLine("Niepoprawny rozmiar pizzy, spróbuj ponownie.");
                Thread.Sleep(1500);
                CreateOrder();
            }

            Pizza pizza = null;
            switch (choice)
            {
                case "1":
                    pizza = new Margherita(size);
                    break;
                case "2":
                    pizza = new Pepperoni(size);
                    break;
                case "3":
                    pizza = new Hawaiian(size);
                    break;
                case "4":
                    pizza = new Capricciosa(size);
                    break;
                case "5":
                    pizza = new JohnnysSpecial(size);
                    break;
                
            }

            Console.Write("\nPracujemy nad tym");
            Thread.Sleep(500);
            for(int i = 0; i < 3; i++)
            {
                Console.Write(".");
                Thread.Sleep(500);
            }
            ActiveOrder = new Order(pizza, this);
            Order.ActiveUntakenOrders.Add(ActiveOrder);
            DB.AddOrder(pizza, this);
            Console.WriteLine($"\nZamówienie zostało dodane! {pizza.GetType().Name} w rozmiarze {size}");
            Thread.Sleep(1500);
            this.clientMenu.Menu(this);
        }

        public void AddReview()
        {
            Console.Clear();
            Console.WriteLine("///////////////////////////////////////////");
            Console.WriteLine("         Dodaj recenzję!      ");
            Console.WriteLine("///////////////////////////////////////////");
            Console.WriteLine("         Wpisz 'esc' by anulować         ");
            Console.WriteLine("///////////////////////////////////////////");
            Console.WriteLine();
            Console.Write("Napisz swoją recenzje: ");

            string? opinion = Console.ReadLine();
            while (true)
            {
                if (!string.IsNullOrEmpty(opinion) && opinion.ToLower() == "esc")
                {
                    Console.WriteLine("Anulowano dodawanie recenzji!");
                    Thread.Sleep(1500);
                    clientMenu.Menu(this);
                    return;
                }
                else if (!string.IsNullOrEmpty(opinion))
                {
                    break;
                }
                Console.Write("Pusto!, spróbuj ponownie: ");
                opinion = Console.ReadLine();
            }

            DB.AddReview(opinion, Username.ToString());
            Console.WriteLine("Recenzja została dodana!");
            Thread.Sleep(1500);
            clientMenu.Menu(this);
        }
    }
}