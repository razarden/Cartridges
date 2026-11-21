using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WpfApp1.Classes
{
    public class Printers
    {
        [Key]
        public int PrinterId { get; set; }
        public string PrinterName { get; set; }
        public string SerialNumber { get; set; }
        public string Location { get; set; }
        public string Status { get; set; }

    }
}
