using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PizzaShop.Data;

namespace PizzaShop.Pages.User
{
    public class DeleteUserModel : PageModel
    {
        private readonly ApplicationDbContext _db;

		public DeleteUserModel(ApplicationDbContext db)
		{
			_db = db;
		}

		public async Task<IActionResult> OnGet(string userId)
        {
            var userToDelete = await _db.ApplicationUsers.FirstOrDefaultAsync(x => x.Id == userId);

            if (userToDelete == null)
            {
                return NotFound();
            }

            _db.ApplicationUsers.Remove(userToDelete);
            _db.SaveChangesAsync();

            return RedirectToPage("/User/GetUsers");
        }
    }
}
