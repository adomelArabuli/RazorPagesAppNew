using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PizzaShop.Data;

namespace PizzaShop.Pages.User
{
    public class LockUnlockModel : PageModel
    {
        private readonly ApplicationDbContext _db;

		public LockUnlockModel(ApplicationDbContext db)
		{
			_db = db;
		}

		public async Task<IActionResult> OnGet(string userId)
        {
            var userToCheck = await _db.ApplicationUsers.FirstOrDefaultAsync(x => x.Id == userId);
            if (userToCheck == null)
            {
                return NotFound("User not fount");
            }
            
            if(userToCheck.LockoutEnd != null && userToCheck.LockoutEnd > DateTime.Now)
            {
                userToCheck.LockoutEnd = DateTime.Now;
            }
            else
            {
                userToCheck.LockoutEnd = DateTime.Now.AddYears(1000);
            }

            await _db.SaveChangesAsync();
            return RedirectToPage("/User/GetUsers");
        }
    }
}
