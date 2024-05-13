using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PizzaShop.Data.Model.ViewModels;
using PizzaShop.Services.Interfaces;
using System.Text.Encodings.Web;

namespace PizzaShop.Pages.Account
{
	public class ForgotPasswordModel : PageModel
	{
		private readonly UserManager<IdentityUser> _userManager;
		private readonly IEmailSender _emailSender;
		public ForgotPasswordModel(UserManager<IdentityUser> userManager, IEmailSender emailSender)
		{
			_userManager = userManager;
			_emailSender = emailSender;
		}

		[BindProperty]
		public ForgotPasswordViewModel Input { get; set; }

		public void OnGetAsync()
		{
		}

		public async Task<IActionResult> OnPostAsync()
		{
			var user = await _userManager.FindByEmailAsync(Input.Email);
			if (user == null)
			{
				return NotFound();
			}
			var code = await _userManager.GeneratePasswordResetTokenAsync(user);
			var callBackUrl = Url.Page(
				"/Account/ResetPassword", 
				pageHandler: null, 
				values: new { userId = user.Id, code = code }, 
				protocol: Request.Scheme);
			await _emailSender.SendEmailAsync(Input.Email, "Reset password - Identity Manager",
					$"Please reset your password by <a href='{HtmlEncoder.Default.Encode(callBackUrl)}'>clicking here</a>.");
			return RedirectToPage("ForgotPasswordConfirmation");
		}
	}
}
