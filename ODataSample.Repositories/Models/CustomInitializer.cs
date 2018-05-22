using ODataSample.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;

namespace ODataSample.Repositories.Models
{
    public class CustomInitializer : DropCreateDatabaseAlways<CustomContext>
    {
        protected override void Seed(CustomContext context)
        {  
            var customer = new Customer()
            {
                FirstName = "John",
                LastName = "Mitchell",
                TelephoneNumber = "34",
                //Products = new[] {
                //    new Product { Category = "DDD", Name = "DDD", Price = 3 }
                //}
            };

            var product = new Product()
            {
                Name = "ApplePhone",
                Category = "Electronics",
                Price = 800
            };

            product.Customers.Add(customer);

            var country = new Country()
            {
                CountryName = "Australia",
                PostalCode = "678",
                Customers = new List<Customer> {
                    customer
                }
            };

            customer.Country = country;

            context.Customers.AddOrUpdate(
                customer
            );

            context.Products.AddOrUpdate(
                product
            );            

            context.Countries.AddOrUpdate(
                country
            );



            base.Seed(context);
        }
    }
}
