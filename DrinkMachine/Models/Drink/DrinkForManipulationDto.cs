namespace DrinkMachine.Models.Drink;

public abstract class DrinkForManipulationDto
{
    public string Name { get; set; } = string.Empty;
    public int Price { get; set; }
    public int Quantity { get; set; }
    public string ImageUrl { get; set; } = string.Empty;
}