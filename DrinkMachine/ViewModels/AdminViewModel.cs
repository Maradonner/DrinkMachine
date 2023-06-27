using DrinkMachine.DAL.Entities;

namespace DrinkMachine.ViewModels;

public class AdminViewModel
{
    public List<Coin> Coins { get; set; } = new();
    public List<Drink> Drinks { get; set; } = new();
}