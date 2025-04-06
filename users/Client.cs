using System;
using System.IO;
using Pizzeria.interfaces;
using Pizzeria.menus;

namespace Pizzeria.users
{
	public class Client : User, IOrderManagment
	{
		public ClientMenu clientMenu { get; set; }

		public Client(string username, string password) : base(username, password, User.Role.Client)
        {
            clientMenu = new ClientMenu();
        }

		public void CreateOrder()
		{

		}

		public void AddReview()
		{

		}
    }
}