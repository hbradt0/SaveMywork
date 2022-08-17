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
            var states = GetStates();
            db.States.AddRange(states);
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

            var customers = GetCustomers(states);
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

        private List<Entries> GetCustomers(List<State> states) {
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

            var citiesStates = new string[]
            {
                "Phoenix,AZ,Arizona",
                "Encinitas,CA,California",
                "Seattle,WA,Washington",
                "Chandler,AZ,Arizona",
                "Dallas,TX,Texas",
                "Orlando,FL,Florida",
                "Carey,NC,North Carolina",
                "Anaheim,CA,California",
                "Dallas,TX,Texas",
                "New York,NY,New York",
                "White Plains,NY,New York",
                "Las Vegas,NV,Nevada",
                "Los Angeles,CA,California",
                "Portland,OR,Oregon",
                "Seattle,WA,Washington",
                "Houston,TX,Texas",
                "Chicago,IL,Illinois",
                "Atlanta,GA,Georgia",
                "Chandler,AZ,Arizona",
                "Buffalo,NY,New York",
                "Albuquerque,AZ,Arizona",
                "Boise,ID,Idaho",
                "Salt Lake City,UT,Utah",
                "Orlando,FL,Florida"
            };

            var citiesIds = new int[] {5, 9, 44, 5, 36, 17, 16, 9, 36, 14, 14, 6, 9, 24, 44, 36, 25, 19, 5, 14, 5, 23, 38, 17};

            var orders = new List<Order> 
            {
                new Order { Product = "Basket", Price = 29.99M, Quantity = 1 },
                new Order { Product = "Yarn", Price = 9.99M, Quantity = 1 },
                new Order { Product = "Needes", Price = 5.99M, Quantity = 1 },
                new Order { Product = "Speakers", Price = 499.99M, Quantity = 1 },
                new Order { Product = "iPod", Price = 399.99M, Quantity = 1 },
                new Order { Product = "Table", Price = 329.99M, Quantity = 1 },
                new Order { Product = "Chair", Price = 129.99M, Quantity = 4 },
                new Order { Product = "Lamp", Price = 89.99M, Quantity = 5 },
                new Order { Product = "Call of Duty", Price = 59.99M, Quantity = 1 },
                new Order { Product = "Controller", Price = 49.99M, Quantity = 1 },
                new Order { Product = "Gears of War", Price = 49.99M, Quantity = 1 },
                new Order { Product = "Lego City", Price = 49.99M, Quantity = 1 },
                new Order { Product = "Baseball", Price = 9.99M, Quantity = 5 },
                new Order { Product = "Bat", Price = 19.99M, Quantity = 1 }
            };

            int firstOrder, lastOrder, tempOrder = 0;
            var ordersLength = orders.Count;
            var customers = new List<Entries>();
            var random = new Random();

            for (var i = 0; i < customerNames.Length; i++) {
                var nameGenderHost = customerNames[i].Split(',');
                var cityState = citiesStates[i].Split(',');
                var state = states.Where(s => s.Abbreviation == cityState[1]).SingleOrDefault();

                var customer = new Entries {
                    Entry = nameGenderHost[0],
                    date = nameGenderHost[1],
                    Email = nameGenderHost[0] + '.' + nameGenderHost[1] + '@' + nameGenderHost[3],
                };

                firstOrder = (int)Math.Floor(random.NextDouble() * orders.Count);
                lastOrder = (int)Math.Floor(random.NextDouble() * orders.Count);

                if (firstOrder > lastOrder) {
                    tempOrder = firstOrder;
                    firstOrder = lastOrder;
                    lastOrder = tempOrder;
                }
                customers.Add(customer);
            }

            return customers;
        }

        private List<State> GetStates() {
            var states = new List<State> 
            {
                new State { Name = "Alabama", Abbreviation = "AL" },
                new State { Name = "Montana", Abbreviation = "MT" },
                new State { Name = "Alaska", Abbreviation = "AK" },
                new State { Name = "Nebraska", Abbreviation = "NE" },
                new State { Name = "Arizona", Abbreviation = "AZ" },
                new State { Name = "Nevada", Abbreviation = "NV" },
                new State { Name = "Arkansas", Abbreviation = "AR" },
                new State { Name = "New Hampshire", Abbreviation = "NH" },
                new State { Name = "California", Abbreviation = "CA" },
                new State { Name = "New Jersey", Abbreviation = "NJ" },
                new State { Name = "Colorado", Abbreviation = "CO" },
                new State { Name = "New Mexico", Abbreviation = "NM" },
                new State { Name = "Connecticut", Abbreviation = "CT" },
                new State { Name = "New York", Abbreviation = "NY" },
                new State { Name = "Delaware", Abbreviation = "DE" },
                new State { Name = "North Carolina", Abbreviation = "NC" },
                new State { Name = "Florida", Abbreviation = "FL" },
                new State { Name = "North Dakota", Abbreviation = "ND" },
                new State { Name = "Georgia", Abbreviation = "GA" },
                new State { Name = "Ohio", Abbreviation = "OH" },
                new State { Name = "Hawaii", Abbreviation = "HI" },
                new State { Name = "Oklahoma", Abbreviation = "OK" },
                new State { Name = "Idaho", Abbreviation = "ID" },
                new State { Name = "Oregon", Abbreviation = "OR" },
                new State { Name = "Illinois", Abbreviation = "IL" },
                new State { Name = "Pennsylvania", Abbreviation = "PA" },
                new State { Name = "Indiana", Abbreviation = "IN" },
                new State { Name = "Rhode Island", Abbreviation = "RI" },
                new State { Name = "Iowa", Abbreviation = "IA" },
                new State { Name = "South Carolina", Abbreviation = "SC" },
                new State { Name = "Kansas", Abbreviation = "KS" },
                new State { Name = "South Dakota", Abbreviation = "SD" },
                new State { Name = "Kentucky", Abbreviation = "KY" },
                new State { Name = "Tennessee", Abbreviation = "TN" },
                new State { Name = "Louisiana", Abbreviation = "LA" },
                new State { Name = "Texas", Abbreviation = "TX" },
                new State { Name = "Maine", Abbreviation = "ME" },
                new State { Name = "Utah", Abbreviation = "UT" },
                new State { Name = "Maryland", Abbreviation = "MD" },
                new State { Name = "Vermont", Abbreviation = "VT" },
                new State { Name = "Massachusetts", Abbreviation = "MA" },
                new State { Name = "Virginia", Abbreviation = "VA" },
                new State { Name = "Michigan", Abbreviation = "MI" },
                new State { Name = "Washington", Abbreviation = "WA" },
                new State { Name = "Minnesota", Abbreviation = "MN" },
                new State { Name = "West Virginia", Abbreviation = "WV" },
                new State { Name = "Mississippi", Abbreviation = "MS" },
                new State { Name = "Wisconsin", Abbreviation = "WI" },
                new State { Name = "Missouri", Abbreviation = "MO" },
                new State { Name = "Wyoming", Abbreviation = "WY" }
            };

            return states;
        }
    }
}
