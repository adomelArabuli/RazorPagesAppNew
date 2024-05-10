using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PizzaShop.Data.Model.ViewModels;
using System.Text.Encodings.Web;

namespace PizzaShop.Pages.Account
{
    public class ProfileModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
		private readonly UrlEncoder _urlEncoder;
		public ProfileModel(UserManager<IdentityUser> userManager, UrlEncoder urlEncoder)
		{
			_userManager = userManager;
			_urlEncoder = urlEncoder;
		}

		public async Task<IActionResult> OnGet()
        {
			var user = await _userManager.GetUserAsync(User);
			if(user is null)
			{
				ViewData["TwoFactorEnabled"] = false;
			}
			else
			{
				ViewData["TwoFactorEnabled"] = user.TwoFactorEnabled;
			}

			return Page();
		}
	}
}
