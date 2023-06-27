using AutoMapper;
using DrinkMachine.DAL.Entities;
using DrinkMachine.Models.Drink;

namespace DrinkMachine.Mapper;

public class DrinkProfiler : Profile
{
    public DrinkProfiler()
    {
        CreateMap<Drink, DrinkForCreationDto>().ReverseMap();
        CreateMap<Drink, DrinkForDisplayDto>().ReverseMap();
        CreateMap<Drink, DrinkForManipulationDto>().ReverseMap();
        CreateMap<Drink, DrinkForUpdateDto>().ReverseMap();
    }
}