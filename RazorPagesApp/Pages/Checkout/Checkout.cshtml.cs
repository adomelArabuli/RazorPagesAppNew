using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata;
using PizzaShop.Data;
using PizzaShop.Data.Model.DbModels;
using System.Reflection.Metadata;

namespace PizzaShop.Pages.Checkout
{
    [BindProperties(SupportsGet = true)]
    public class CheckoutModel : PageModel
    {
        public string? PizzaName { get; set; }
        public float PizzaPrice { get; set; }
        public string? ImageTitle { get; set; }

        private readonly ApplicationDbContext _db;

        public CheckoutModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public void OnGet(string? pizzaName, float pizzaPrice, string? imageTitle)
        {
            PizzaName = pizzaName;
            PizzaPrice = pizzaPrice;
            ImageTitle = imageTitle;
        }
        public IActionResult Onpost(string? pizzaName, float FinalPrice, string? imageTitle)
        {
            if (string.IsNullOrEmpty(pizzaName))
            {
                PizzaName = "Custom";
            }

            if (string.IsNullOrWhiteSpace(imageTitle))
            {
                ImageTitle = "pizzaParts";
            }

            PizzaOrder pizzaOrder = new PizzaOrder();
            pizzaOrder.PizzaName = PizzaName;
            pizzaOrder.PizzaPrice = FinalPrice;

            _db.PizzaOrders.Add(pizzaOrder);
            _db.SaveChanges();

            return RedirectToPage("/Checkout/Checkout", new { pizzaName = pizzaName, pizzaPrice = FinalPrice, imageTitle = imageTitle });
        }

    }
}
