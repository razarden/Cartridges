using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using WpfApp1.View;
using WpfApp1.Models;
using WpfApp1.Classes;
using System.Windows;
using System.ComponentModel.DataAnnotations;
using System.Windows.Controls;
using WebApplication2.Models;

namespace WpfApp1.ViewsModel
{
    public static class SaveChangeCartrige
    {
        public static async Task<bool> Save(EditCartrigeWindow editwindow, Cartriges cartriges) 
        {
            if (string.IsNullOrWhiteSpace(editwindow.Edit_Model.Text)) 
            {
                MessageBox.Show("Выберите модель картриджа");
                return false;
            }
            if (string.IsNullOrWhiteSpace(editwindow.Edit_SerialNumber.Text))
            {
                MessageBox.Show("Введите серийный номер картриджа");
                return false;
            }
            if (string.IsNullOrWhiteSpace(editwindow.Edit_Printer.Text))
            {
                MessageBox.Show("Выберите модель принтера");
                return false;
            }
            if (string.IsNullOrWhiteSpace(editwindow.Edit_DataPurchase.Text))
            {
                MessageBox.Show("Выберите дату покупки принтера");
                return false;
            }
            if (string.IsNullOrWhiteSpace(editwindow.Edit_Status.Text))
            {
                MessageBox.Show("Выберите статус принтера");
                return false;
            }
            cartriges.Model = editwindow.Edit_Model.Text;
            cartriges.SerialNumber = editwindow.Edit_SerialNumber.Text;
            cartriges.DataPurchase = Convert.ToDateTime(editwindow.Edit_DataPurchase.Text);
            cartriges.Status = editwindow.Edit_Status.Text;

            if (editwindow.Edit_Printer.SelectedValue != null) 
            {
                cartriges.PrinterId = (int)editwindow.Edit_Printer.SelectedValue;
            }
            else 
            {
                cartriges.PrinterId = null;
            }
            ComboBoxItem selectedStatus = (ComboBoxItem)editwindow.Edit_Status.SelectedItem;
            cartriges.Status = selectedStatus.Tag.ToString();
            cartriges.Printers = null;
            
            using (var db = new DbConext()) 
            {
                db.Cartriges.Update(cartriges);
                await db.SaveChangesAsync();
            }
            return true;

        }
    }
}
