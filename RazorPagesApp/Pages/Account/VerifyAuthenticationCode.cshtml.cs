using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PizzaShop.Data.Model.ViewModels;

namespace PizzaShop.Pages.Account
{
    public class VerifyAuthenticationCodeModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;

		public VerifyAuthenticationCodeModel(SignInManager<IdentityUser> signInManager)
		{
			_signInManager = signInManager;
		}

		public VerifyAuthenticatorViewModel Model = new VerifyAuthenticatorViewModel();

		public async Task<IActionResult> OnGetAsync(bool rememberMe, string? returnUrl)
        {
            var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();

            if (user is null)
            {
				return NotFound($"User not found");
			}

			Model.ReturnUrl = returnUrl;
			Model.RememberMe = rememberMe;

			return Page();
		}

		public async Task<IActionResult> OnPostAsync(VerifyAuthenticatorViewModel model)
		{
			model.ReturnUrl = model.ReturnUrl ?? Url.Content("~/");

			if(!ModelState.IsValid)
			{
				return Page();
			}

			var result = await _signInManager.TwoFactorAuthenticatorSignInAsync(model.Code, model.RememberMe, rememberClient: true);

			if (result.Succeeded)
			{
				return LocalRedirect(model.ReturnUrl);
			}
			else if(result.IsLockedOut)
			{
				return RedirectToPage("/Account/Lockout");
			}
			else
			{
				ModelState.AddModelError(string.Empty, "Invalid code");
				return Page();
			}
		}
    }
}
