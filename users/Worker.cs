using System;
using System.IO;
using Pizzeria.interfaces;
using Pizzeria.menus;
using Pizzeria.order;

namespace Pizzeria.users
{
    public class Worker : User, IOrderManagment
    {
        public WorkerMenu workerMenu { get; set; }
        public Order ActiveOrder { get; set; }

        public Worker(string username, string password) : base(username, password, User.Role.Worker)
        {
            workerMenu = new WorkerMenu();
        }

        public void OrderPreview()
        {
            if (ActiveOrder == null)
            {
                Console.WriteLine("Nie masz aktywnego zamówienia.");
                return;
            }
            else
            {
                Console.WriteLine("Aktywne zamówienie:");
                Console.WriteLine($"Zamówienie nr {ActiveOrder.Id}");
                Console.WriteLine($"Pizza: {ActiveOrder.Pizza.Name}");
                Console.WriteLine($"Data zamówienia: {ActiveOrder.DateOfOrder}");
            }


        }

        public void TakeOrder()
        {
            if (ActiveOrder != null)
            {
                Console.WriteLine("Masz już aktywne zamówienie.");
                return;
            }
            if (Order.ActiveUntakenOrders.Count == 0)
            {
                Console.WriteLine("Wszystkie zamówienia są już zajęte lub wykonane.");
                return;
            }

            Console.Clear();
            Console.WriteLine("///////////////////////////////////////////");
            Console.WriteLine("         Podejmij się zamówienia!      ");
            Console.WriteLine("///////////////////////////////////////////");
            Console.WriteLine();
            foreach (var order in Order.ActiveUntakenOrders)
            {
                Console.WriteLine($"Zamówienie nr {order.Id}");
                Console.WriteLine($"Pizza: {order.Pizza.Name}");
                Console.WriteLine($"Data zamówienia: {order.DateOfOrder}");
                Console.WriteLine();
            }
            Console.Write("Podaj numer zamówienia: ");


            bool success = false;
            while (!success)
            {
                string orderNumber = Console.ReadLine();
                while (string.IsNullOrEmpty(orderNumber))
                {
                    Console.Write("Pusto!, spróbuj ponownie: ");
                    orderNumber = Console.ReadLine();
                }
                foreach(var order in Order.ActiveUntakenOrders)
                {
                    if (order.Id.ToString() == orderNumber)
                    {
                        ActiveOrder = order;
                        ActiveOrder.AssignedWorker = this;
                        Order.ActiveUntakenOrders.Remove(order);
                        success = true;
                        break;
                    }
                }
            }
            Console.WriteLine($"Zamówienie nr {ActiveOrder.Id} zostało podjęte pomyślnie.");

            
        }
        public void MakePizza()
        {
            if (ActiveOrder == null)
            {
                Console.WriteLine("Nie masz aktywnego zamówienia.");
                return;
            }
            else
            {
                Console.WriteLine($"Pizza {ActiveOrder.Pizza.Name} została wykonana.");
                ActiveOrder = null;
            }
        }
    }
}