using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PizzaShop.Data.Model.ViewModels;

namespace PizzaShop.Pages.Account
{
    public class LockoutModel : PageModel
    {
        public LockoutInfoViewModel LockoutInfo { get; set; }
        public void OnGet()
        {
            //LockoutInfo = TempData.Get<LockoutInfoViewModel>("LockoutInfo");
        }
    }
}
