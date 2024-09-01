using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car_Traders
{
    internal class Order
    {
        public int OrderID { get; set; }
        public int UserID { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string ProductName { get; set; }
        public string ProductType { get; set; }
        public decimal Price { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;
    }
}
