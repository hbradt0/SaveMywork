using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Angular_ASPNETCore_CustomersService.Models;

namespace Angular_ASPNETCore_CustomersService.Repository
{
    public class CustomersDbSeeder
    {
        readonly ILogger _Logger;

        public CustomersDbSeeder(ILoggerFactory loggerFactory)
        {
            _Logger = loggerFactory.CreateLogger("CustomersDbSeederLogger");
        }

        public async Task SeedAsync(IServiceProvider serviceProvider)
        {
            //Based on EF team's example at https://github.com/aspnet/MusicStore/blob/dev/samples/MusicStore/Models/SampleData.cs
            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var customersDb = serviceScope.ServiceProvider.GetService<CustomersDbContext>();
                if (await customersDb.Database.EnsureCreatedAsync())
                {
                    if (!await customersDb.Customers.AnyAsync()) {
                      await InsertCustomersSampleData(customersDb);
                    }
                }
            }
        }

        public async Task InsertCustomersSampleData(CustomersDbContext db)
        {

            try
            {
                int numAffected = await db.SaveChangesAsync();
                _Logger.LogInformation(@"Saved {numAffected} states");
            }
            catch (Exception exp)
            {                
                _Logger.LogError($"Error in {nameof(CustomersDbSeeder)}: " + exp.Message);
                throw; 
            }

            var customers = GetCustomers();
            db.Customers.AddRange(customers);

            try
            {
              int numAffected = await db.SaveChangesAsync();
                _Logger.LogInformation($"Saved {numAffected} customers");
            }
            catch (Exception exp)
            {
              _Logger.LogError($"Error in {nameof(CustomersDbSeeder)}: " + exp.Message);
              throw;
            }

        }

        private List<Entries> GetCustomers() {
            //Customers
            var customerNames = new string[]
            {
                "Marcus,HighTower,Male,acmecorp.com",
                "Jesse,Smith,Female,gmail.com",            
            };

            var dates = new string[]
            {
                "1/2/3",
                "1/2/3"
            };

          var emails = new string[]
          {
              "acd@email.com",
              "abcd@gmail.com",
          };  

          var customers = new List<Entries>();

            for (var i = 0; i < customerNames.Length; i++) {
                var customer = new Entries {
                    entry = customerNames[i],
                    mydate = dates[i],
                    email = emails[i],
                };
                customers.Add(customer);
            }
            return customers;
        }

    }
}
