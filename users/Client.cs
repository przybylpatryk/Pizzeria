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

        //konstruktor klasy Client
        public Client(string username, string password) : base(username, password, User.Role.Client)
        {
            clientMenu = new ClientMenu();
            DB = new Database();
        }

        //metoda służąca do towrzenia zamówienia
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

            //sprawdza czy użytkownik nie dał nic
            while (string.IsNullOrEmpty(choice))
            {
                Console.Write("Pusto!, spróbuj ponownie: ");
                choice = Console.ReadLine();
            }

            //sprawdza czy użytkownik podał poprawną wartość
            if (choice != "1" && choice != "2" && choice != "3" && choice != "4" && choice != "5" && choice != "6")
            {
                Console.WriteLine("Niepoprawny wybór, spróbuj ponownie.");
                Thread.Sleep(1500);
                CreateOrder();
            }

            //anuluje zamówienia w przypadku '6'
            if(choice == "6")
            {
                Console.WriteLine("Anulowano zamówienie!");
                Thread.Sleep(1500);
                clientMenu.Menu(this);
            }
            Console.WriteLine("Podaj rozmiar pizzy (Mała, Średnia, Duża, Bardzo Duża)");

            //sprawdza czy użytkownik podał poprawny rozmiar pizzy
            string? sizeInput = Console.ReadLine().ToLower();
            while (string.IsNullOrEmpty(sizeInput) || (sizeInput != "mała" && sizeInput != "średnia" && sizeInput != "duża" && sizeInput != "bardzo duża")) 
            {
                Console.Write("Niepoprawny rozmiar pizzy, spróbuj ponownie (Mała, Średnia, Duża, Bardzo Duża): ");
                sizeInput = Console.ReadLine().ToLower();
            }

            //zmienia rozmiar pizzy na poprawną wartość dla enuma
            switch (sizeInput)
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
            //Konweruje stringa na enuma
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
            //przypisuje wybraną pizze do zmiennej pizza
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
            clientMenu.Menu(this);
        }

        //metoda służąca do dodawania recenzji
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
                //sprawdza czy użytkownik podał 'esc' i anuluje dodawanie recenzji
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

        //metoda służąca do przeglądania recenzji
        public void GetReviews()
        {
            Console.Clear();
            Console.WriteLine("///////////////////////////////////////////");
            Console.WriteLine("         Recenzje!      ");
            Console.WriteLine("///////////////////////////////////////////");
            Console.WriteLine();
            DB.GetReviews();
            Console.WriteLine();
            Console.WriteLine("Naciśnij dowolny klawisz aby wrócić do menu.");
            Console.ReadKey();
            clientMenu.Menu(this);
        }

        //metoda służąca do odbierania zamówienia
        public void GetOrderForC()
        {
            Console.Clear();
            Console.WriteLine("///////////////////////////////////////////");
            Console.WriteLine("         Zamówienia!      ");
            Console.WriteLine("///////////////////////////////////////////");
            Console.WriteLine();

            List<string> orderID = DB.GetOrderIDForC(this);

            //sprawdza czy są zamówienia dostępne do odbioru
            if (orderID.Count == 0)
            {
                Console.WriteLine("Brak zamówień do przyjęcia!");
                Thread.Sleep(1500);
                clientMenu.Menu(this);
                return;
            }

            DB.GetOrderForC(this);
            Console.WriteLine();
            Console.WriteLine("Podaj ID zamówienia, które chcesz przyjąć:");

            string? orderW = Console.ReadLine();
            while (true)
            {

                //sprawdza poprawność ID, które podał użytkownik
                if (!orderID.Contains(orderW))
                {
                    Console.WriteLine("Niepoprawne ID zamówienia.");
                    Thread.Sleep(1500);
                    clientMenu.Menu(this);
                    return;
                }

                //sprawdza czy użytkownik wpisał 'esc' i wtedy anuluje akcję
                if (!string.IsNullOrEmpty(orderW) && orderW.ToLower() == "esc")
                {
                    Console.WriteLine("Anulowano!");
                    Thread.Sleep(1500);
                    clientMenu.Menu(this);
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
            DB.RecieveOrder(orderW, this);
            Console.WriteLine($"Zamówienie o ID {orderW} zostało przyjęte!");
            Thread.Sleep(1500);
            clientMenu.Menu(this);
        }

        //metoda służąca do przeglądania zamówienia
        public void OrderPreview()
        {
            Console.Clear();
            Console.WriteLine("///////////////////////////////////////////");
            Console.WriteLine("         Zamówienia!      ");
            Console.WriteLine("///////////////////////////////////////////");
            Console.WriteLine();
            DB.GetOrderInfoForC(this);
            Console.WriteLine();
            Console.WriteLine("Naciśnij dowolny klawisz aby wrócić do menu.");
            Console.ReadKey();
            clientMenu.Menu(this);
        }
    }
}