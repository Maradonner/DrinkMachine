namespace DrinkMachine.Models.Coin;

public class CoinForDisplayDto : CoinForManipulationDto
{
    public int Id { get; set; }
    public int Value { get; set; }
    public long Quantity { get; set; }
    public bool IsBlocked { get; set; }
    public string ImageUrl { get; set; } = string.Empty;
}