using AutoMapper;
using DrinkMachine.DAL.Entities;
using DrinkMachine.Models.Coin;

namespace DrinkMachine.Mapper;

public class CoinProfiler : Profile
{
    public CoinProfiler()
    {
        CreateMap<Coin, CoinForDisplayDto>().ReverseMap();
        CreateMap<Coin, CoinForManipulationDto>().ReverseMap();
        CreateMap<Coin, CoinForUpdateDto>().ReverseMap();
    }
}