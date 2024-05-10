using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PizzaShop.Data;
using PizzaShop.Data.Model.ViewModels;
using System.Data;

namespace PizzaShop.Pages.User
{
	public class EditUserModel : PageModel
	{
		private readonly ApplicationDbContext _db;
		private readonly UserManager<IdentityUser> _userManager;
		public EditUserModel(ApplicationDbContext db, UserManager<IdentityUser> userManager)
		{
			_db = db;
			_userManager = userManager;
		}

		[BindProperty]
		public UserEditViewModel userViewModel { get; set; } = new();

		public async Task<IActionResult> OnGetAsync(string userId)
		{
			var userToEdit = await _db.ApplicationUsers.FirstOrDefaultAsync(u => u.Id == userId);
			if (userToEdit is null)
			{
				return NotFound("User not fount");
			}

			var userRoles = await _db.UserRoles.ToListAsync();
			var roles = await _db.Roles.ToListAsync();
			var userRole = await _db.UserRoles.FirstOrDefaultAsync(ur => ur.UserId == userId);

			if (userRole != null)
			{
				userViewModel.Id = userToEdit.Id;
				userViewModel.UserName = userToEdit.UserName;
				userViewModel.FirstName = userToEdit.FirstName;
				userViewModel.LastName = userToEdit.LastName;
				userViewModel.Role = roles.FirstOrDefault(r => r.Id == userRole.RoleId).Name;
				userViewModel.LockoutEnd = userToEdit.LockoutEnd;
			}

			userViewModel.RoleList = roles.Select(r => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
			{
				Text = r.Name,
				Value = r.Name
			});

			return Page();

		}

		public async Task<IActionResult> OnPostAsync()
		{
			if (ModelState.IsValid)
			{
				var userToEdit = await _db.ApplicationUsers.FirstOrDefaultAsync(u => u.Id == userViewModel.Id);
				if (userToEdit is null)
				{
					return NotFound();
				}

				var userRole = await _db.UserRoles.FirstOrDefaultAsync(ur => ur.UserId == userToEdit.Id);

				if (userRole != null)
				{
					var previousRoleName = await _db.Roles.Where(x => x.Id == userRole.RoleId).Select(x => x.Name).FirstOrDefaultAsync();

					//Remove old role
					await _userManager.RemoveFromRoleAsync(userToEdit, previousRoleName);


				}

				//Add new role
				await _userManager.AddToRoleAsync(userToEdit, userViewModel.Role);

				userToEdit.FirstName = userViewModel.FirstName;
				userToEdit.LastName = userViewModel.LastName;
				await _db.SaveChangesAsync();

				return RedirectToPage("/User/GetUsers");
			}

			return Page();
		}
	}
}
