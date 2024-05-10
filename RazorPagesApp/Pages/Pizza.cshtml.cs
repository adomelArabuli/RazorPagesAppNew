using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PizzaShop.Data.Model.ViewModels;

namespace PizzaShop.Pages
{
    public class PizzaModel : PageModel
    {
        public List<PizzaDbModel> fakePizzaDB = new List<PizzaDbModel>()
        {
            new PizzaDbModel()
            {
                ImageTitle = "margarita", 
                PizzaName = "Margarita",
                Tuna = true,
                Cheese = true,
                FinalPrice = 4
            },
            new PizzaDbModel()
            {
                ImageTitle = "hawaiian",
                PizzaName = "Hawaiian",
                TomatoSauce = true,
                Beef = true,
                FinalPrice = 4
            },
            new PizzaDbModel()
            {
                ImageTitle = "vegetarian",
                PizzaName = "Vegetarian",
                Pepperoni = true,
                Mushroom = true,
                FinalPrice = 4
            },
            new PizzaDbModel()
            {
                ImageTitle = "seafood",
                PizzaName = "Seafood",
                Tuna = true,
                Cheese = true,
                FinalPrice = 4
            },
            new PizzaDbModel()
            {
                ImageTitle = "pepperoni",
                PizzaName = "Pepperoni",
                Tuna = true,
                Cheese = true,
                FinalPrice = 11
            },
            new PizzaDbModel()
            {
                ImageTitle = "mushroom",
                PizzaName = "Mushroom",
                Mushroom = true,
                Cheese = true,
                FinalPrice = 7
            }
        };


        public void OnGet()
        {
        }
    }
}
