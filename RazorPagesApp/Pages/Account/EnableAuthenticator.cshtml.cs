using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using PizzaShop.Data.Model.ViewModels;
using System.Text.Encodings.Web;

namespace PizzaShop.Pages.Account
{
    public class EnableAuthenticatorModel : PageModel
    {
		private readonly UserManager<IdentityUser> _userManager;
		private readonly UrlEncoder _urlEncoder;

		public EnableAuthenticatorModel(UserManager<IdentityUser> userManager, UrlEncoder urlEncoder)
		{
			_userManager = userManager;
			_urlEncoder = urlEncoder;
		}

		public TwoFactorAuthenticationViewModel Model = new TwoFactorAuthenticationViewModel();

		public async Task<IActionResult> OnGetAsync()
        {
			// 2 - secret, 1 -email, 0 - issuer : For QR code generation
			// 
			string authenticatorUriFormat = "otpauth://totp/{0}:{1}?secret={2}&issuer={0}&digits=4";

			var user = await _userManager.GetUserAsync(User);

			// resetting any previously registered authentication
			// User - application global user
			await _userManager.ResetAuthenticatorKeyAsync(user);

			//Generate a new token
			var token = await _userManager.GetAuthenticatorKeyAsync(user);

			string authenticatorUri = string.Format(authenticatorUriFormat, _urlEncoder.Encode("PizzaShop"),
				_urlEncoder.Encode(user.Email), token);

			var model = new TwoFactorAuthenticationViewModel() { Token = token, QRCodeUrl = authenticatorUri };

			Model = model;

			return Page();
		}

		public async Task<IActionResult> OnPostAsync(TwoFactorAuthenticationViewModel model)
		{
			if(ModelState.IsValid)
			{
				var user = await _userManager.GetUserAsync(User);

				var succeeded = await _userManager.VerifyTwoFactorTokenAsync(user, _userManager.Options.Tokens.AuthenticatorTokenProvider, model.Code);

				if(succeeded)
				{
					var result = await _userManager.SetTwoFactorEnabledAsync(user, true);
				}
				else
				{
					ModelState.AddModelError("Verify", "Your two factor auth code could not be validated");
					return Page();
				}
			}
			return RedirectToPage("/Account/AuthenticationConfirmation");
		}
    }
}
