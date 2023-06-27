using AutoMapper;
using DrinkMachine.BL.Services.Interfaces;
using DrinkMachine.DAL.Entities;
using DrinkMachine.Middleware;
using DrinkMachine.Models.Coin;
using DrinkMachine.Models.Drink;
using DrinkMachine.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DrinkMachine.Controllers;

public class AdminController : Controller
{
    private readonly ICoinService _coinService;
    private readonly IDrinkService _drinkService;
    private readonly ILogger<AdminController> _logger;
    private readonly IMapper _mapper;

    public AdminController(IDrinkService drinkService, ILogger<AdminController> logger,
        ICoinService coinService, IMapper mapper)
    {
        _drinkService = drinkService;
        _logger = logger;
        _coinService = coinService;
        _mapper = mapper;
    }


    [SecretKeyAuthorize]
    [HttpGet]
    public async Task<IActionResult> Index(CancellationToken ct)
    {

        var drinks = await _drinkService.GetAllDrinksAsync(ct);
        var coins = await _coinService.GetCoinsListAsync(ct);

        return View(new AdminViewModel
        {
            Coins = coins,
            Drinks = drinks
        });
    }

    [HttpPost("{id}")]
    public async Task<ActionResult<Drink>> GetDrink(int id, CancellationToken ct)
    {
        var drink = await _drinkService.GetDrinkByIdAsync(id, ct);
        if (drink == null)
            return NotFound();
        return drink;
    }

    [HttpPost]
    public async Task<ActionResult<DrinkForDisplayDto>> AddDrink(DrinkForCreationDto drinkForCreationDto,
        CancellationToken ct)
    {
        var drink = _mapper.Map<Drink>(drinkForCreationDto);
        try
        {
            var result = await _drinkService.AddDrinkAsync(drink, ct);
            if (!result)
                return BadRequest("Error occured!");
        }
        catch (DbUpdateException)
        {
            _logger.LogError("Error when starting add {drink}", drink);
            return BadRequest("Error occured!");
        }

        var drinkForDisplay = _mapper.Map<DrinkForDisplayDto>(drink);

        return drinkForDisplay;
    }

    [HttpPost]
    public async Task<ActionResult<DrinkForDisplayDto>> UpdateDrink(
        DrinkForUpdateDto drinkForUpdateDto, CancellationToken ct)
    {
        var drink = _mapper.Map<Drink>(drinkForUpdateDto);

        try
        {
            var result = await _drinkService.UpdateDrinkAsync(drink, ct);
            if (!result)
                return BadRequest("Error occured!");
        }
        catch (DbUpdateException)
        {
            _logger.LogError("Error when starting add {drink}", drink);
            return BadRequest("Error occured!");
        }

        var drinkForDisplayDto = _mapper.Map<DrinkForDisplayDto>(drink);

        return drinkForDisplayDto;
    }

    [HttpPost]
    public async Task<ActionResult<bool>> DeleteDrink(int id, CancellationToken ct)
    {
        try
        {
            var result = await _drinkService.DeleteDrinkAsync(id, ct);
            if (!result)
                return BadRequest("Error occured!");
        }
        catch (DbUpdateException)
        {
            _logger.LogError("Error when starting add {drink}", id);
            return BadRequest("Error occured!");
        }

        return true;
    }

    [HttpPost]
    public async Task<ActionResult<CoinForDisplayDto>> ToggleBlockCoin(int coinId,
        CancellationToken ct)
    {
        var coin = await _coinService.GetCoinByIdAsync(coinId, ct);

        if (coin == null)
            return NotFound("Coin is not found");

        coin.IsBlocked = !coin.IsBlocked;

        try
        {
            var result = await _coinService.UpdateCoinAsync(coin, ct);
            if (!result)
                return BadRequest("Error occured!");
        }
        catch (DbUpdateException)
        {
            _logger.LogError("Error when starting add {coin}", coin);
            return BadRequest("Error occured!");
        }

        var coinForDisplay = _mapper.Map<CoinForDisplayDto>(coin);

        return coinForDisplay;
    }
}