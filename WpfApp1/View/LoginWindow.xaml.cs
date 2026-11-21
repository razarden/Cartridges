using Microsoft.SqlServer;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WebApplication2.Models;

namespace WpfApp1
{
    
    public partial class LoginWindow : Window
    {
        private readonly string connectionString = "Server = srv-sql2; Database=user97_1;User Id = user97; Password=tecuser97;TrustServerCertificate=True;";
        private readonly DbConext dbConext = new DbConext();

        public LoginWindow()
        {
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string role = Role.Text;
            string name = Name.Text;
            string pass = password.Text;
            dbConext.Logout(name,role,pass);
             
        }
    }
}
