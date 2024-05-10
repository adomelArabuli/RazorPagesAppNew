using Microsoft.AspNetCore.Identity;

namespace PizzaShop.Data.Model.DbModels
{
	public class ApplicationUser : IdentityUser
	{
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
