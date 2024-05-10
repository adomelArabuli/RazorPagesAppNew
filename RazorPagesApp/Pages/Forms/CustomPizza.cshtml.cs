using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PizzaShop.Data;
using PizzaShop.Data.Model.DbModels;
using PizzaShop.Data.Model.ViewModels;

namespace PizzaShop.Pages.Forms
{
    public class CustomPizzaModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        [BindProperty]
        public PizzaDbModel Pizza { get; set; }
        public float PizzaPrice { get; set; }

        public CustomPizzaModel(ApplicationDbContext db)
        {
            _db = db; 
        }
        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            PizzaPrice = Pizza.BasePrice;

            var boolProperties = typeof(PizzaDbModel).GetProperties().Where(p => p.PropertyType == typeof(bool));

            #region oldCode
            //foreach (var prop in boolProperties)
            //{
            //    bool value = (bool)prop.GetValue(Pizza);
            //    if (value)
            //        PizzaPrice += 1;
            //}
            #endregion

            PizzaPrice += boolProperties
                .Where(p => (bool)p.GetValue(Pizza))
                .Sum(p => 1);

            #region comment
            //if (Pizza.TomatoSauce)PizzaPrice += 1;
            //if (Pizza.Cheese)PizzaPrice += 2;
            //if(Pizza.Tuna) PizzaPrice += 1;
            //if(Pizza.Pepperoni) PizzaPrice += 2;
            //if(Pizza.Ham) PizzaPrice += 4;
            //if(Pizza.Beef) PizzaPrice += 3;
            //if(Pizza.Mushroom) PizzaPrice += 1;
            #endregion

            var pizzaOrder = new PizzaOrder
            {
                PizzaName = Pizza.PizzaName,
                PizzaPrice = PizzaPrice,
                TomatoSauce = Pizza.TomatoSauce,
                Cheese = Pizza.Cheese,
                Pepperoni = Pizza.Pepperoni,
                Mushroom = Pizza.Mushroom,
                Tuna = Pizza.Tuna,
                Ham = Pizza.Ham,
                Beef = Pizza.Beef
            };

            _db.PizzaOrders.Add(pizzaOrder);
            _db.SaveChanges();

            return RedirectToPage("/Checkout/Checkout", new { Pizza.PizzaName, PizzaPrice });
        }
    }
}
