namespace DrinkMachine.DAL.Entities;

public class Coin
{
    public int Id { get; set; }
    public int Value { get; set; }
    public bool IsBlocked { get; set; }
    public string ImageUrl { get; set; } = string.Empty;
}