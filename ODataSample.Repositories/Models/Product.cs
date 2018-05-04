using ODataSample.Repositories.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace ODataSample.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }

        [ForeignKey("Customer")]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        public Product(Customer customer)
        {
            Customer = customer;
        }
        public Product()
        {
        }
    }
}