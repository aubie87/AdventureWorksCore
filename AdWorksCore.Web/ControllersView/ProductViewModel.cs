using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdWorksCore.Web.Models;

namespace AdWorksCore.Web.ControllersView
{
    public class ProductViewModel
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        [Required, MinLength(4)]
        public string ProductNumber { get; set; }
        public decimal ListPrice { get; set; }
        public string Color { get; set; }

        public Product ToProduct()
        {
            return new Product()
            {
                Id = this.Id,
                Color = this.Color,
                ListPrice = this.ListPrice,
                Name = this.Name,
                ProductNumber = this.ProductNumber
            };
        }
    }
}
