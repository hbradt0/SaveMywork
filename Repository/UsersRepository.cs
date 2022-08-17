using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;
using Angular_ASPNETCore_CustomersService.Models;

namespace Angular_ASPNETCore_CustomersService.Repository
{
    public class UsersRepository : IUsersRepository
    {

        private readonly CustomersDbContext _Context;
        private readonly ILogger _Logger;

        public UsersRepository(CustomersDbContext context, ILoggerFactory loggerFactory) {
          _Context = context;
          _Logger = loggerFactory.CreateLogger("UsersRepository");
        }

        public async Task<List<User>> GetCustomersAsync()
        {
            return await _Context.Users.OrderBy(c => c.Email)
                                 .Include(c => c.Password).ToListAsync();
        }

        public async Task<PagingResult<User>> GetCustomersPageAsync(int skip, int take)
        {
            var totalRecords = await _Context.Customers.CountAsync();
            var customers = await _Context.Users
                                 .OrderBy(c => c.Email)
                                 .Include(c => c.Password)
                                 .Skip(skip)
                                 .Take(take)
                                 .ToListAsync();
            return new PagingResult<User>(customers, totalRecords);
        }

        public async Task<User> GetCustomerAsync(String email)
        {
            return await _Context.Users
                                 .Include(c => c.Email)
                                 .SingleOrDefaultAsync(c => c.Email == email);
        }

        public async Task<User> InsertCustomerAsync(User customer)
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

        public async Task<bool> UpdateCustomerAsync(User customer)
        {
            //Will update all properties of the Customer
            _Context.Users.Attach(customer);
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

        public async Task<bool> DeleteCustomerAsync(String email)
        {
            //Extra hop to the database but keeps it nice and simple for this demo
            //Including orders since there's a foreign-key constraint and we need
            //to remove the orders in addition to the customer
            var customer = await _Context.Users
                                .Include(c => c.Email)
                                .SingleOrDefaultAsync(c => c.Email == email);
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
