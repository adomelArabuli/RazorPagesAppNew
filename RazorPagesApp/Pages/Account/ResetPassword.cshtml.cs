using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using PizzaShop.Data.Model.ViewModels;

namespace PizzaShop.Pages.Account
{
    public class ResetPasswordModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;

		public ResetPasswordModel(UserManager<IdentityUser> userManager)
		{
			_userManager = userManager;
		}

		[BindProperty]
        public ResetPasswordViewModel Input { get;set; }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.FindByEmailAsync(Input.Email);

            if (user == null)
            {
                return NotFound();
            }

            var result = await _userManager.ResetPasswordAsync(user, Input.Code, Input.Password);

            if (result.Succeeded)
            {
                return RedirectToPage("ResetPasswordConfirmation");
            }

            return Page();
        }
    }
}
