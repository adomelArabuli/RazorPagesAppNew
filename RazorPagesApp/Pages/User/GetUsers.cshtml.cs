using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PizzaShop.Data;
using PizzaShop.Data.Model.DbModels;
using PizzaShop.Data.Model.ViewModels;

namespace PizzaShop.Pages.User
{
	[Authorize(Policy = "RequireAdminRole")]
	public class GetUsersModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ApplicationDbContext _db;

        public GetUsersModel(UserManager<IdentityUser> userManager, ApplicationDbContext db)
        {
            _userManager = userManager;
            _db = db;
        }

        public List<UserViewModel> Users = new List<UserViewModel>();

        public async Task<IActionResult> OnGetAsync()
        {
            var users = await _db.ApplicationUsers.ToListAsync();
            var userRoles = await _db.UserRoles.ToListAsync();
            var roles = await _db.Roles.ToListAsync();

            foreach (var user in users)
            {
                var userVM = new UserViewModel();
                var role = userRoles.FirstOrDefault(u => u.UserId == user.Id);

                userVM.Id = user.Id;
                userVM.UserName = user.UserName;
                userVM.FirstName = user.FirstName;
                userVM.LastName = user.LastName;
                userVM.Role = roles.FirstOrDefault(r => r.Id == role.RoleId).Name;
                userVM.EmailConfirmed = user.EmailConfirmed;
                userVM.TwoFactorEnabled = user.TwoFactorEnabled;
                userVM.LockoutEnd = user.LockoutEnd;

                Users.Add(userVM);
            }
            return Page();
        }
    }
}
