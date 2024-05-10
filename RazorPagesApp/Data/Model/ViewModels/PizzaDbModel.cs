using System.ComponentModel.DataAnnotations;

namespace PizzaShop.Data.Model.ViewModels
{
    public class PizzaDbModel
    {
        public string? ImageTitle { get; set; }
        [Required]
        [MinLength(2, ErrorMessage = "მინიმალური სიმბოლოების რაოდენობა = 2")]
        public string? PizzaName { get; set; }
        public float BasePrice { get; set; } = 5;
        public bool TomatoSauce { get; set; }
        public bool Cheese { get; set; }
        public bool Pepperoni { get; set; }
        public bool Mushroom { get; set; }
        public bool Tuna { get; set; }
        public bool Ham { get; set; }
        public bool Beef { get; set; }
        public float FinalPrice { get; set; }
    }
}
