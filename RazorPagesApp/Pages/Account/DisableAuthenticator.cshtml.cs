using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PizzaShop.Pages.Account
{
    public class DisableAuthenticatorModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;

		public DisableAuthenticatorModel(UserManager<IdentityUser> userManager)
		{
			_userManager = userManager;
		}

		public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);

            await _userManager.ResetAuthenticatorKeyAsync(user);
            await _userManager.SetTwoFactorEnabledAsync(user, false);

            return RedirectToPage("/Index");
        }
    }
}
