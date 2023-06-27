using DrinkMachine.DAL.Entities;

namespace DrinkMachine.ViewModels;

public class DisplayViewModel
{
    public List<Drink> Drinks { get; set; } = new();
    public List<Coin> Coins { get; set; } = new();
    public long Balance { get; set; }
}