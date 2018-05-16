using ODataSample.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ODataSample.Repositories.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string TelephoneNumber { get; set; }

        public virtual ICollection<Product> Products { get; set; }

        [ForeignKey("Country")]
        public int CountryId { get; set; }
        public Country Country { get; set; }

        public Customer(Country country)
        {
            Country = country;
            Products = new HashSet<Product>();
        }
        public Customer()
        {
            Products = new HashSet<Product>();
        }
    }
}
