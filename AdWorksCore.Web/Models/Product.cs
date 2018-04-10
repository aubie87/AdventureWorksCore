using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdWorksCore.Web.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ProductNumber { get; set; }
        public decimal ListPrice { get; set; }
        public string Color { get; set; }
    }
}
