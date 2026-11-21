using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WebApplication2.Models;
using WpfApp1.Classes;
using WpfApp1.Models;
using WpfApp1.ViewsModel;

namespace WpfApp1.View
{
    /// <summary>
    /// Логика взаимодействия для EditCartrigeWindow.xaml
    /// </summary>
    public partial class EditCartrigeWindow : Window
    {
        public Cartriges _currentcartrige;
        public EditCartrigeWindow(Cartriges cartrige)
        {
            InitializeComponent();
            _currentcartrige = cartrige;
            LoadDataForEditCartrige();
        }
        private async void LoadDataForEditCartrige() 
        {
            using (var db = new DbConext()) 
            {
                Edit_Model.ItemsSource = await db.Cartriges.Select(c=>c.Model).Distinct().ToListAsync();
                Edit_Printer.ItemsSource = await db.Printers.ToListAsync();
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private async void SaveChange_Click(object sender, RoutedEventArgs e)
        {
            bool isSaved = await SaveChangeCartrige.Save(this,_currentcartrige);
            if (isSaved) 
            {
                MessageBox.Show("Данные успешно изменены");
                this.DialogResult = true;
            }
            else 
            {
                MessageBox.Show("Ошибка изменения данных картриджа");
            }
        }
    }
}
