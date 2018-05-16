using ODataSample.Repositories.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ODataSample.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }

        public virtual ICollection<Customer> Customers { get; set; }

        public Product(IList<Customer> customer)
        {
            Customers = new HashSet<Customer>(customer);
        }
        public Product()
        {
            Customers = new HashSet<Customer>();
        }
    }
}