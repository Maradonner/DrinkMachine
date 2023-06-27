using DrinkMachine.BL.Services.Interfaces;
using DrinkMachine.DAL.Entities;
using DrinkMachine.Models.Coin;
using DrinkMachine.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace DrinkMachine.Controllers;

public class UserController : Controller
{
    private readonly IDbSessionService _dbSessionService;
    private readonly ICoinService _coinService;
    private readonly IDrinkService _drinkService;

    public UserController(ICoinService coinService, IDbSessionService dbSessionService, IDrinkService drinkService)
    {
        _coinService = coinService;
        _drinkService = drinkService;
        _dbSessionService = dbSessionService;
        _dbSessionService.GetSessionAsync().GetAwaiter().GetResult();
    }

    [HttpGet]
    public async Task<IActionResult> Index(CancellationToken ct)
    {
        await _dbSessionService.GetSessionAsync();

        var session = await _dbSessionService.GetSessionAsync();

        var coins = await _coinService.GetCoinsListAsync(ct);
        var drinks = await _drinkService.GetAllDrinksAsync(ct);

        var balance = session.Balance;


        return View(new DisplayViewModel
        {
            Coins = coins,
            Drinks = drinks,
            Balance = balance
        });
    }

    [HttpPost]
    public async Task<ActionResult<long>> DepositCoin(int id, CancellationToken ct)
    {
        var coin = await _coinService.GetCoinByIdAsync(id, ct);

        if (coin == null || coin.IsBlocked)
            return BadRequest("Монета заблокирована или не найдена");

        await _dbSessionService.UpdateBalanceAsync(coin.Value);
        var session = await _dbSessionService.GetSessionAsync();
        return session.Balance;
    }

    [HttpPost]
    public async Task<ActionResult<object>> PurchaseDrink(int id, CancellationToken ct)
    {
        var session = await _dbSessionService.GetSessionAsync();

        var drink = await _drinkService.GetDrinkByIdAsync(id, ct);

        if (drink == null || session.Balance < drink.Price || drink.Quantity <= 0)
            return BadRequest("Напиток не найден, цена оказалась больше, чем баланс, или напитки закончились");

        drink.Quantity -= 1;

        await _drinkService.UpdateDrinkAsync(drink, ct);
        await _drinkService.SaveChangesAsync(ct);
        await _dbSessionService.UpdateBalanceAsync(-drink.Price);

        return new
        {
            session.Balance,
            Drink = drink
        };
    }

    [HttpPost]
    public async Task<ActionResult<List<CoinForDisplayDto>>> GetChange(CancellationToken ct)
    {
        var coins = await _coinService.GetCoinsListAsync(ct);
        var session = await _dbSessionService.GetSessionAsync();

        if (session.Balance <= 0)
            return BadRequest("Непозитивный баланс");

        var change = GetChangeSimple(coins, session.Balance);

        await _dbSessionService.UpdateBalanceAsync((int)-session.Balance);

        return change;
    }

    private static List<CoinForDisplayDto> GetChangeSimple(List<Coin> coins, long change)
    {
        List<CoinForDisplayDto> coinList = new();
        var valuesCoins = coins.OrderByDescending(x => x.Value).ToList();

        foreach (var coin in valuesCoins)
        {
            long count = 0;
            count += change / coin.Value;
            change %= coin.Value;

            if (count != 0)
                coinList.Add(new CoinForDisplayDto
                {
                    Quantity = count,
                    Value = coin.Value,
                    ImageUrl = coin.ImageUrl
                });

            if (change == 0)
                return coinList;
        }

        return coinList;
    }
}