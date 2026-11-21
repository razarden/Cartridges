using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.SqlServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using WebApplication2.Models;
using WpfApp1.Models;
using WpfApp1.Classes;
using WpfApp1.View;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            Loaded += MainWindow_Loaded;
        }

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }
        private async Task LoadData() 
        {
            try 
            {
                using (var db = new DbConext())
                {
                    var data = await db.Cartriges.Include(c=>c.Printers).ToListAsync();
                    CartrigeGrid.ItemsSource = data;
                }
            }
            catch (Exception ex) 
            {
                MessageBox.Show($"Ошибка загрузки данных о картриджах: {ex.Message}");
            }
        }
        private void GotFocus_search(object sender, RoutedEventArgs e) 
        {
            if (search_text.Text == "Поиск по модели, принтеру...")
                search_text.Text = "";
        }
        private void LostFocus_search(object sender, RoutedEventArgs e)
        {
            if (search_text.Text == "")
                search_text.Text = "Поиск по модели, принтеру...";
        }

        //Это метод который вызывает метод для сортировки текста
        private async void Text_Changed_Search_NeededRows(object sender, TextChangedEventArgs e) 
        {
            string quary = search_text.Text.Trim();
            if (CartrigeGrid == null)
                return;
            CartrigeGrid.ItemsSource = await Search.Search_Rows(quary);
        }

        private void EditRows_Click(object sender, RoutedEventArgs e) 
        {
            Cartriges _selectedcartrige = (Cartriges)CartrigeGrid.SelectedItem;

            if (_selectedcartrige == null) 
            {
                MessageBox.Show("Сначала выберите картридж, чтобы приступить к его редактированию");
                return;
            }
            EditCartrigeWindow editwindow = new EditCartrigeWindow(_selectedcartrige);
            editwindow.Owner = this;

            if (editwindow.ShowDialog() == true)
                LoadData();
        }
        private async void Delete_Row(object sender, RoutedEventArgs e) 
        {
            Cartriges _selectedcartrige = (Cartriges)CartrigeGrid.SelectedItem;
            if (_selectedcartrige == null)
            {
                MessageBox.Show("Сначала выберите картридж, чтобы удалить его");
                return;
            }
            using (var db = new DbConext()) 
            {
                db.Cartriges.Remove(_selectedcartrige);
                await db.SaveChangesAsync();
                LoadData();
            }
        }

    }
}
