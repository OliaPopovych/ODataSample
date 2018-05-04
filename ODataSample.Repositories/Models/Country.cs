using System.Collections.Generic;

namespace ODataSample.Repositories.Models
{
    public class Country
    {
        public int CountryId { get; set; }
        public string PostalCode { get; set; }
        public string CountryName { get; set; }

        public ICollection<Customer> Customers { get; set; }

        public Country()
        {
            Customers = new List<Customer>();
        }
    }
}
