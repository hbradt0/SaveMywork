using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;
using Angular_ASPNETCore_CustomersService.Models;

namespace Angular_ASPNETCore_CustomersService.Repository
{
    public class CustomersRepository : ICustomersRepository
    {

        private readonly CustomersDbContext _Context;
        private readonly ILogger _Logger;

        public CustomersRepository(CustomersDbContext context, ILoggerFactory loggerFactory) {
          _Context = context;
          _Logger = loggerFactory.CreateLogger("CustomersRepository");
        }

        public async Task<List<Entries>> GetCustomersAsync()
        {
            return await _Context.Customers.OrderBy(c => c.entry).ToListAsync();
        }

        public async Task<PagingResult<Entries>> GetCustomersPageAsync(int skip, int take)
        {
           // var totalRecords = await _Context.Customers.CountAsync();
            var customers = await _Context.Customers
                                 //.OrderBy(c => c.entry)
                                 //.Include(c => c.entry)
                                 .Skip(skip)
                                 .Take(take)
                                 .ToListAsync();
            return new PagingResult<Entries>(customers, 10);
        }

        public async Task<Entries> GetCustomerAsync(int id)
        {
            return await _Context.Customers
                                 //.Include(c => c.entry)
                                 .SingleOrDefaultAsync(c => c.id == id);
        }

        public async Task<Entries> InsertCustomerAsync(Entries customer)
        {
            _Context.Add(customer);
            try
            {
              await _Context.SaveChangesAsync();
            }
            catch (System.Exception exp)
            {
               _Logger.LogError($"Error in {nameof(InsertCustomerAsync)}: " + exp.Message);
            }

            return customer;
        }

        public async Task<bool> UpdateCustomerAsync(Entries customer)
        {
            //Will update all properties of the Customer
            _Context.Customers.Attach(customer);
            _Context.Entry(customer).State = EntityState.Modified;
            try
            {
              return (await _Context.SaveChangesAsync() > 0 ? true : false);
            }
            catch (Exception exp)
            {
               _Logger.LogError($"Error in {nameof(UpdateCustomerAsync)}: " + exp.Message);
            }
            return false;
        }

        public async Task<bool> DeleteCustomerAsync(int id)
        {
            //Extra hop to the database but keeps it nice and simple for this demo
            //Including orders since there's a foreign-key constraint and we need
            //to remove the orders in addition to the customer
            var customer = await _Context.Customers
                                //.Include(c => c.entry)
                                .SingleOrDefaultAsync(c => c.id == id);
            _Context.Remove(customer);
            try
            {
              return (await _Context.SaveChangesAsync() > 0 ? true : false);
            }
            catch (System.Exception exp)
            {
               _Logger.LogError($"Error in {nameof(DeleteCustomerAsync)}: " + exp.Message);
            }
            return false;
        }

    }
}
