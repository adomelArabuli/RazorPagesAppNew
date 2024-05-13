using System.ComponentModel.DataAnnotations;

namespace PizzaShop.Data.Model.ViewModels
{
	public class ForgotPasswordViewModel
	{
		[Required]
		[EmailAddress]
        public string Email { get; set; }
    }
}
