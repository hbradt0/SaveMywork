using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Angular_ASPNETCore_CustomersService.Models {
  public class Entries
  {
    public int id { get; set; }
    public string entry { get; set; }
    public string email { get; set; }
    public string mydate { get; set; }

  }
  public class User
  {
    public int id { get; set; }
    public string email { get; set; }
    public string password { get; set; }
  }
}
