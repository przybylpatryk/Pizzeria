using System;
using System.IO;
using Pizzeria.interfaces;
using Pizzeria.menus;
using Pizzeria.database;

namespace Pizzeria.users
{
	public class Client : User, IOrderManagment
	{
		public ClientMenu clientMenu { get; set; }
        public Database DB { get; set; }

        public Client(string username, string password) : base(username, password, User.Role.Client)
        {
            clientMenu = new ClientMenu();
            DB = new Database();
        }

		public void CreateOrder()
		{
			
		}

		public void AddReview()
		{
			Console.Clear();
            Console.WriteLine("///////////////////////////////////////////");
            Console.WriteLine("         Dodaj recenzję!      ");
            Console.WriteLine("///////////////////////////////////////////");
            Console.WriteLine();
            Console.Write("Napisz swoją recenzje: ");

            string? opinion = Console.ReadLine();
            while (string.IsNullOrEmpty(opinion))
            {
                Console.Write("Pusto!, spróbuj ponownie: ");
                opinion = Console.ReadLine();
            }

            DB.AddReview(opinion, this.Username.ToString());
            Console.WriteLine("Recenzja została dodana!");
            Thread.Sleep(1500);
            this.clientMenu.Menu(this);
        }
    }
}