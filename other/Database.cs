using System;
using MySqlConnector;
using Pizzeria.users;
using System.Collections.Generic;
using System.Threading;
using Pizzeria.order;
using Pizzeria.pizza;
using Pizzeria.interfaces;
using System.IO;
using Pizzeria.menus;
using System.Security.Cryptography.X509Certificates;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Pizzeria.database
{
    public class Database
    {
        //dane do łączenia sie z bazą
        private readonly string connectionString = "Server=localhost;Database=pizzeria;User=root;Password=;";

        public MySqlConnection Conn { get; set; }

        //przy tworzeniu nowej bazy automatycznie sie łączy i otwiera
        public Database()
        {
            try
            {
                Conn = new MySqlConnection(connectionString);
                Conn.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd połączenia z bazą: {ex.Message}");
            }
        }

        //tutaj będą metody związane z bazami danych
        public List<Worker> GetWorkers()
        {
            List<Worker> workers = new List<Worker>();

            string query = "SELECT * FROM pracownicy";
            MySqlCommand result = new MySqlCommand(query, Conn);
            MySqlDataReader row = result.ExecuteReader();
            while (row.Read())
            {
                Worker worker = new Worker(row["Nazwa"].ToString(), row["Haslo"].ToString());
                worker.UserRole = User.Role.Worker;
                workers.Add(worker);
            }
            row.Close();
            return workers;
        }

        public List<Client> GetClients()
        {
            List<Client> clients = new List<Client>();

            string query = "SELECT * FROM klienci";
            MySqlCommand result = new MySqlCommand(query, Conn);
            MySqlDataReader row = result.ExecuteReader();
            while (row.Read())
            {
                Client client = new Client(row["Nazwa"].ToString(), row["Haslo"].ToString());
                client.UserRole = User.Role.Client;
                clients.Add(client);
            }
            row.Close();
            return clients;
        }

        public List<Admin> GetAdmins()
        {
            List<Admin> admins = new List<Admin>();

            string query = "SELECT * FROM admini";
            MySqlCommand result = new MySqlCommand(query, Conn);
            MySqlDataReader row = result.ExecuteReader();
            while (row.Read())
            {
                Admin admin = new Admin(row["Nazwa"].ToString(), row["Haslo"].ToString());
                admin.UserRole = User.Role.Admin;
                admins.Add(admin);
            }
            row.Close();
            return admins;
        }

        public void AddClient(string username, string password)
        {
            string query = $"INSERT INTO klienci (ID, Nazwa, Haslo) VALUES (NULL, '{username}', '{password}')";
            MySqlCommand result = new MySqlCommand(query, Conn);
            result.ExecuteNonQuery();
        }

        //public List<Order> GetOrderPreview(int ID)
        //{
        //    string query = $"SELECT ID, RodzajPizzy, Data_zamowienia FROM zamowienia WHERE ID = {ID}";
        //    MySqlCommand result = new MySqlCommand(query, Conn);
        //    MySqlDataReader row = result.ExecuteReader();
        //    List<Order> orders = new List<Order>();
        //    while (row.Read())
        //    {
        //        Order order = new Order(row["ID"].ToString(), row["Rodzaj_pizzy"].ToString(), row["ID_pracownika"].ToString(), row["ID_klienta"].ToString(), row["Data_zamowienia"].ToString());
        //        orders.Add(order);
        //    }
        //    row.Close();
        //    return orders;
        //}

        public void AddReview(string review, string clientUsername)
        {
            string query = $"INSERT INTO opinie (ID, Nazwa_klienta, opinia) VALUES (ID, '{clientUsername}', '{review}')";
            MySqlCommand result = new MySqlCommand(query, Conn);
            result.ExecuteNonQuery();
        }

        public void HireWorker(string name, string password)
        {
            string query = $"INSERT INTO pracownicy (ID, Nazwa, Haslo) VALUES (NULL, '{name}', '{password}')";
            MySqlCommand result = new MySqlCommand(query, Conn);
            result.ExecuteNonQuery();
        }

        public void FireWorker(string name, string password)
        {
            string query = $"DELETE FROM pracownicy WHERE Nazwa='{name}' AND Haslo = '{password}';";
            MySqlCommand result = new MySqlCommand(query, Conn);
            result.ExecuteNonQuery();
        }

        //public List<Order> GetOrder()
        //{
        //    string query = $"SELECT * FROM zamowienia";
        //    MySqlCommand result = new MySqlCommand(query, Conn);
        //    MySqlDataReader row = result.ExecuteReader();
        //    while (row.Read())
        //    {
        //        Console.WriteLine($"ID: {row["ID"]}, Rodzaj pizzy: {row["Rodzaj_pizzy"]}, Data zamówienia: {row["Data_zamowienia"]}");
        //    }
        //    row.Close();
        //}

        //public void AddOrder(Pizza pizza, Client client, DateTime date)
        //{
        //    string query = $"INSERT INTO zamowienia (ID, Rodzaj_pizzy, Nazwa_klienta, Data_zamowienia) VALUES (ID, '{pizza.Name}', '{client.Username}', '{date}')";
        //    MySqlCommand result = new MySqlCommand(query, Conn);
        //    result.ExecuteNonQuery();
        //}

        public void IncreaseSalary(string name, string password, int salary)
        {
            string query = $"UPDATE pracownicy SET Placa = Placa + {salary} WHERE Nazwa='{name}' AND Haslo = '{password}';";
            MySqlCommand result = new MySqlCommand(query, Conn);
            result.ExecuteNonQuery();
        }
    }
}