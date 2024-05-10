using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using PizzaShop.Data.Model.DbModels;
using PizzaShop.Services.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Xml.Linq;

namespace PizzaShop.Pages.Account
{
	public class RegisterModel : PageModel
	{
		private readonly SignInManager<IdentityUser> _signInManager;
		private readonly UserManager<IdentityUser> _userManager;
		private readonly RoleManager<IdentityRole> _roleManager;
		private readonly IUserStore<IdentityUser> _userStore;
		private readonly IUserEmailStore<IdentityUser> _emailStore;
		private readonly IEmailSender _emailSender;
        public RegisterModel(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager, IUserStore<IdentityUser> userStore, IEmailSender emailSender)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
            _userStore = userStore;
            _emailStore = GetEmailStore();
            _emailSender = emailSender;
        }

        [BindProperty]
		public RegisterInput Input { get; set; }

		public string ReturnUrl { get; set; }

		public IList<AuthenticationScheme> ExternalLogins { get; set; }
		public async Task OnGetAsync(string? returnUrl = null)
		{
			var roles = _roleManager.Roles.ToList();

			if(!roles.Any())
			{
				var adminRole = new IdentityRole("Admin");
				var userRole = new IdentityRole("User");

				await _roleManager.CreateAsync(adminRole);
				await _roleManager.CreateAsync(userRole);
			}

			ReturnUrl = returnUrl;

			ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
			Input = new RegisterInput
			{
				RoleList = _roleManager.Roles.Select(x => x.Name).Select(r => new SelectListItem
				{
					Text = r,
					Value = r
				})
			};
		}

		public async Task<IActionResult> OnPostAsync(string? returnUrl = null)
		{
			returnUrl ??= Url.Content("~/");
			ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

			if (ModelState.IsValid)
			{
				var user = Activator.CreateInstance<ApplicationUser>();
				await _userStore.SetUserNameAsync(user, Input.Email, CancellationToken.None);
				await _emailStore.SetEmailAsync(user, Input.Email, CancellationToken.None);

				user.FirstName = Input.FirstName;
				user.LastName = Input.LastName;

				var result = await _userManager.CreateAsync(user, Input.Password);

				if (result.Succeeded)
				{
					if (Input.Role is null)
					{
						await _userManager.AddToRoleAsync(user, "User");
					}
					else
					{
						await _userManager.AddToRoleAsync(user, Input.Role);
					}
				}

				var code = _userManager.GenerateEmailConfirmationTokenAsync(user);
                //var callBackUrl = Url.Action("ConfirmEmail", null, new { userId = user.Id, code = code.Result }, protocol: HttpContext.Request.Scheme);

                var callBackUrl = Url.Page("/Account/ConfirmEmail", pageHandler: null, values: new { userId = user.Id, code = code.Result }, protocol: Request.Scheme);


                //await _emailSender.SendEmailAsync(Input.Email,
                //	"Confirm your account - Identity Manager",
                //		"Please confirm your account by clicking on the following link: <a href=\"" + callBackUrl + "\">Click here</a>");
                await _emailSender.SendEmailAsync(Input.Email, 
					"Confirm your account - Identity Manager",
					$"Please confirm your account by clicking on the following link: <a href=\"{HtmlEncoder.Default.Encode(callBackUrl)}\">Click here</a>");


                await _signInManager.SignInAsync(user, isPersistent: false);

			}
			return Page();
		}

		private IUserEmailStore<IdentityUser> GetEmailStore()
		{
			if (!_userManager.SupportsUserEmail)
			{
				throw new NotSupportedException("The default UI requires a user store with email support.");
			}
			return (IUserEmailStore<IdentityUser>)_userStore;
		}
	}

	public class RegisterInput
	{
		[Required]
		[EmailAddress]
		[Display(Name = "Email")]
		public string Email { get; set; }

		[Required]
		[StringLength(30, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long", MinimumLength = 6)]
		[DataType(DataType.Password)]
		[Display(Name = "Password")]
		public string Password { get; set; }

		[DataType(DataType.Password)]
		[Display(Name = "Confirm Password")]
		[Compare("Password", ErrorMessage = "The password and confirmation password do not match")]
		public string ConfirmPassword { get; set; }

		[Required]
		public string FirstName { get; set; }

		[Required]
		public string LastName { get; set; }

		public string? Role { get; set; }

		public IEnumerable<SelectListItem>? RoleList { get; set; }
	}


}
