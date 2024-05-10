namespace PizzaShop.Data.Model.ViewModels
{
    public class LockoutInfoViewModel
    {
        public bool IsLockedOut { get; set; }
        public TimeSpan TimeUntilUnlock { get; set; }
    }
}
