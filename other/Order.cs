using Pizzeria.pizza;
using Pizzeria.users;
using System;
using System.Security.Cryptography.X509Certificates;

namespace Pizzeria.order
{
    public class Order
    {
        public int Id { get; set; }
        public Pizza Pizza { get; set; }
        public Worker AssignedWorker { get; set; }
        public Client AssignedClient { get; set; }
        private DateTime date;
        public static List<Order> ActiveUntakenOrders = new List<Order>();

        public DateTime DateOfOrder
        {
            get { return date.Date; }
            set { date = value.Date; }
        }
        //konstruktor klasy Order
        public Order(Pizza Pizza, Client AssignedClient)
        {
            this.Pizza = Pizza;
            this.AssignedClient = AssignedClient;
        }

    }
}