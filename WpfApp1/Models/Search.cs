using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using WebApplication2.Models;
using WpfApp1;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WpfApp1.Classes;
namespace WpfApp1.Models
{
    public static class Search
    {

        //Это метод который выполняет мгновенный, повторюсь, МГНОВЕННЫЙ поиск нужных нам строк
        public static async Task<List<Cartriges>> Search_Rows(string quary) 
        {
            using (var db = new DbConext()) 
            {
                if (string.IsNullOrEmpty(quary) || quary == "Поиск по модели, принтеру...") 
                {
                    await Task.Delay(300);
                    return await db.Cartriges.Include(c=> c.Printers).ToListAsync();
                }
                return await db.Cartriges.Include(c=>c.Printers).Where(cartrige => cartrige.Model.Contains(quary) || (cartrige.Printers!= null && cartrige.Printers.PrinterName.Contains(quary)) || cartrige.Status.Contains(quary)).ToListAsync();
            }
        }
    }
}
