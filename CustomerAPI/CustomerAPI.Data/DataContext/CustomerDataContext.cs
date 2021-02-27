using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace CustomerAPI.Data.DataContext
{
    public class CustomerDataContext: DbContext
    {
        public DbSet<Customer> Customer { get; set; }

        public CustomerDataContext(DbContextOptions<CustomerDataContext> options)
            : base(options)
        {
            var customerDataCount = Customer.ToListAsync().ConfigureAwait(false).GetAwaiter().GetResult().Count;
            if (customerDataCount == 0)
            {
                List<Customer> customers;
                CreateCustomerData(out customers);
                Customer.AddRange(customers);
                SaveChanges();
            }
            
        }

        private void CreateCustomerData(out List<Customer> customers)
        {
            customers = new List<Customer> {
                new Customer
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Alejandro",
                    LastName = "De Hombre",
                    PhoneNumber = "13256663252",
                    Email = "adehombre@gmail.com",
                    Age = 32
                },
                new Customer
                {
                    Id = Guid.NewGuid() ,
                    FirstName = "Amanda",
                    LastName = "Perez",
                    PhoneNumber = "10222663252",
                    Email = "amandaperez@gmail.com",
                    Age = 30
                }
            };
        }
    }
}
