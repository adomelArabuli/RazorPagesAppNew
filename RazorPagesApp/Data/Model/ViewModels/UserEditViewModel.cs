using Microsoft.AspNetCore.Mvc.Rendering;

namespace PizzaShop.Data.Model.ViewModels
{
	public class UserEditViewModel
	{
		public string Id { get; set; }
		public string UserName { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
        public string Role { get; set; }
		public DateTimeOffset? LockoutEnd { get; set; }
		public IEnumerable<SelectListItem>? RoleList { get; set; }
	}
}
