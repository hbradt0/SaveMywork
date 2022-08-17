using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Angular_ASPNETCore_CustomersService.Models {
  public class Entries
  {
    public int Id { get; set; }
    public string Entry { get; set; }
    public string Email { get; set; }
    public string date { get; set; }

  }

  public class User
  {
    public int Id { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
  }
}
