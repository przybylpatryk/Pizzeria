using System;
using System.Text.RegularExpressions;
using System.IO;
using Pizzeria.interfaces;
using Pizzeria.menus;
using Pizzeria.database;

namespace Pizzeria.users
{
    public class Admin : User, IOrderManagment, IWorkerManagment
    {
        public AdminMenu adminMenu { get; set; }
        public Database DB { get; set; }

        public Admin(string username, string password) : base(username, password, User.Role.Admin)
        {
            adminMenu = new AdminMenu();
            DB = new Database();
        }

        public void HireWorker()
        {
            Console.Clear();
            Console.WriteLine("///////////////////////////////////////////");
            Console.WriteLine("         Prosimy o zarejestrowanie się      ");
            Console.WriteLine("///////////////////////////////////////////");
            Console.WriteLine();
            Console.Write("Podaj nazwe nowego pracownika: ");

            //sprawdzanie czy użytkownik nie klika poprostu entera, lub czy też nazwa nie jest zajęta
            string? username = Console.ReadLine();
            while (string.IsNullOrEmpty(username) || isnotfree(username))
            {
                Console.Write("Pusto, lub ta nazwa jest już zajęta, spróbuj ponownie: ");
                username = Console.ReadLine();
            }
            Console.Write("\nHasło nowego pracownika: ");

            //sprawdzanie czy użytkownik nie klika poprostu entera, lub czy też hasło nie jest za słabe
            string? password = Console.ReadLine();
            while (string.IsNullOrEmpty(password) || notmatches(password))
            {
                Console.Write("Pusto, lub za słabe hasło, spróbuj ponownie: ");
                password = Console.ReadLine();
            }

            DB.HireWorker(username, password);
            Console.WriteLine("Dodano pracownika pomyślnie!");
            Thread.Sleep(1500);
            this.adminMenu.Menu(this);
        }

        public void FireWorker()
        {

        }

        public void IncreaseSalary()
        {

        }

        public static bool isnotfree(string username)
        {
            Database db = new Database();
            List<Client> clients = db.GetClients();
            List<Worker> workers = db.GetWorkers();
            List<Admin> admins = db.GetAdmins();
            if (clients.Any(c => c.Username == username) || workers.Any(w => w.Username == username) || admins.Any(a => a.Username == username))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool notmatches(string password)
        {
            Regex regex = new Regex(@"^(?=.*\d)(?=.*[A-Z])(?=.*[!@#$%^&*()_+={}\[\]:;\'<>,.?/\\|`~-]).{5,}$");
            if (regex.IsMatch(password))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}