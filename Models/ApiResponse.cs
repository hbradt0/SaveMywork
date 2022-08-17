using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Angular_ASPNETCore_CustomersService.Models
{
    public class ApiResponse
    {
        public bool Status { get; set; }
        public Entries Customer { get; set; }
        public User user { get; set; }
        public ModelStateDictionary ModelState { get; set; }
    }
}
