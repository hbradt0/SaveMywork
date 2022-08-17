using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Angular_ASPNETCore_CustomersService.Models;

namespace Angular_ASPNETCore_CustomersService.Repository
{
    public interface ICustomersRepository
    {     
        Task<List<Entries>> GetCustomersAsync();
        Task<PagingResult<Entries>> GetCustomersPageAsync(int skip, int take);
        Task<Entries> GetCustomerAsync(int id);
        
        Task<Entries> InsertCustomerAsync(Entries customer);
        Task<bool> UpdateCustomerAsync(Entries customer);
        Task<bool> DeleteCustomerAsync(int id);
    }
}