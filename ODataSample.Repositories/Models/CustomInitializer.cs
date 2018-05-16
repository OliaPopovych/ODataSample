using ODataSample.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;

namespace ODataSample.Repositories.Models
{
    public class CustomInitializer : CreateDatabaseIfNotExists<CustomContext>
    {
        protected override void Seed(CustomContext context)
        {  
            var customer = new Customer()
            {
                CustomerId = 1,
                FirstName = "John",
                LastName = "Mitchell",
                TelephoneNumber = "34"
            };

            var product = new Product()
            {
                ProductId = 1,
                Name = "ApplePhone",
                Category = "Electronics",
                Price = 800
            };

            var country = new Country()
            {
                CountryId = 1,
                CountryName = "Australia",
                PostalCode = "678",
                Customers = new List<Customer> {
                    customer
                }
            };

            customer.Country = country;

            context.Products.AddOrUpdate(
                product
            );

            context.Customers.AddOrUpdate(
                customer
            );

            context.Countries.AddOrUpdate(
                country
            );



            base.Seed(context);
        }
    }
}
