using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Classes 
{
    public class Cartriges
    {
        public int Id { get; set; }
        public string Model { get; set; }
        public string SerialNumber { get; set; }
        public int? PrinterId { get; set; }

        [ForeignKey("PrinterId")]
        public Printers? Printers { get; set; }
        public DateTime DataPurchase {  get; set; }
        public string Status { get; set; }

    }
}
 

