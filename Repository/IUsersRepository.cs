using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Angular_ASPNETCore_CustomersService.Models;

namespace Angular_ASPNETCore_CustomersService.Repository
{
    public interface IUsersRepository
    {     
        Task<List<User>> GetCustomersAsync();
        Task<PagingResult<User>> GetCustomersPageAsync(int skip, int take);
        Task<User> GetCustomerAsync(String email);
        
        Task<User> InsertCustomerAsync(User customer);
        Task<bool> UpdateCustomerAsync(User customer);
        Task<bool> DeleteCustomerAsync(String email);
    }
}
