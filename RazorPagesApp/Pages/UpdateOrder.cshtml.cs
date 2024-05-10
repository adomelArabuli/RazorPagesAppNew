using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PizzaShop.Data;
using System.Collections.Generic;
using System.Linq;
using PizzaShop.Data.Model.DbModels;

namespace PizzaShop.Pages
{
    public class UpdateOrderModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public UpdateOrderModel(ApplicationDbContext db)
        {
            _db = db;
        }

        [BindProperty(SupportsGet = true)]
        public int OrderId { get; set; }

        [BindProperty]
        public string NewPizzaName { get; set; }

        [BindProperty]
        public float NewPizzaPrice { get; set; }

        [BindProperty]
        public List<string> SelectedIngredients { get; set; }

        public List<string> AllIngredients { get; } = new List<string> { "Tomato Sauce", "Cheese", "Pepperoni", "Mushroom", "Tuna", "Ham", "Beef" };

        public IActionResult OnGet()
        {
            var order = _db.PizzaOrders.Find(OrderId);
            if (order is null)
            {
                return RedirectToPage("/NotFound");
            }

            NewPizzaName = order.PizzaName;
            NewPizzaPrice = (float)order.PizzaPrice;
            SelectedIngredients = AllIngredients.Where(ingredient => IsIngredientSelected(order, ingredient)).ToList();

            return Page();
        }

        public IActionResult OnPost()
        {
            var order = _db.PizzaOrders.Find(OrderId);
            if (order is null)
            {
                return RedirectToPage("/NotFound");
            }

            order.PizzaName = NewPizzaName;

            NewPizzaPrice = CalculatePizzaPrice();

            order.PizzaPrice = NewPizzaPrice;

            foreach (var ingredient in AllIngredients)
            {
                SetIngredientValue(order, ingredient, SelectedIngredients.Contains(ingredient));
            }

            _db.SaveChanges();

            return RedirectToPage("/Order");
        }
        private float CalculatePizzaPrice()
        {
            float pizzaPrice = 0;


            if (SelectedIngredients.Contains("Tomato Sauce"))
                pizzaPrice += 1;
            if (SelectedIngredients.Contains("Cheese"))
                pizzaPrice += 2;
            if (SelectedIngredients.Contains("Pepperoni"))
                pizzaPrice += 2;
            if (SelectedIngredients.Contains("Mushroom"))
                pizzaPrice += 1;
            if (SelectedIngredients.Contains("Tuna"))
                pizzaPrice += 1;
            if (SelectedIngredients.Contains("Ham"))
                pizzaPrice += 4;
            if (SelectedIngredients.Contains("Beef"))
                pizzaPrice += 3;

            return pizzaPrice;
        }


        private bool IsIngredientSelected(PizzaOrder order, string ingredient)
        {
            return (bool)typeof(PizzaOrder).GetProperty(ingredient.Replace(" ", "")).GetValue(order);
        }

        private void SetIngredientValue(PizzaOrder order, string ingredient, bool value)
        {
            typeof(PizzaOrder).GetProperty(ingredient.Replace(" ", "")).SetValue(order, value);
        }
    }
}
