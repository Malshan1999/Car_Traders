using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car_Traders
{
    internal class DbConnection
    {
        
        private static readonly string _connectionString = @"Server=ASUS\SQLEXPRESS;Database=PrimeCar Traders;Trusted_Connection=True;MultipleActiveResultSets=true;";

       
        public static string GetConnectionString()
        {
            return _connectionString;
        }
    }
}
