using System.Data;
using System;
using System.IO;
using Microsoft.SqlServer;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Data.SqlClient;
using System.Windows;
using WpfApp1.Classes;
using WpfApp1;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;


namespace WebApplication2.Models
{
    public class DbConext: DbContext
    {
       
        public DbSet<Cartriges> Cartriges { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Printers> Printers { get; set; } = null!;

        public static string connectionString;

        static DbConext() 
        {
            var configuration = new ConfigurationBuilder().SetBasePath(AppDomain.CurrentDomain.BaseDirectory).AddJsonFile("appsettings.json", optional: true).Build();
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured) 
            {
                optionsBuilder.UseSqlServer(connectionString);
            }
        }
        public void Logout(string name, string role, string password)
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                     
                    string quary = "Select Count(1) from Users Where Name = @name and Role = @role and Password = @password";
                    using (SqlCommand command = new SqlCommand(quary, sqlConnection))
                    {
                         
                        command.Parameters.AddWithValue("@Name", name);
                        command.Parameters.AddWithValue("@Role", role);
                        command.Parameters.AddWithValue("@Password", password);
                        try 
                        {
                            sqlConnection.Open();
                            int count = Convert.ToInt32(command.ExecuteScalar());
                            if (count == 1) 
                            {
                                MainWindow mainWindow = new MainWindow();
                                mainWindow.Show();
                            }
                            else
                            {
                                MessageBox.Show("Неудалось проверить ваши данные, попробуйте чуть позже");
                            }
                        }
                        catch (Exception ex) 
                        {
                            MessageBox.Show($"Неудалось проверить ваши данные, попробуйте чуть позже");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }


        public void Registration(string name,string role, string password) 
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {

                    string quary = "Insert Into Users (Name,Role,Password) Values (@Name,@Role,@Password)";
                    using (SqlCommand command = new SqlCommand(quary, sqlConnection))
                    {
                        sqlConnection.Open();
                        command.Parameters.AddWithValue("@Name", name);
                        command.Parameters.AddWithValue("@Role", role);
                        command.Parameters.AddWithValue("@Password", password);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                 
            }
        }

       
    }
}
