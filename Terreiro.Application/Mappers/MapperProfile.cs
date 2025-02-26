using AutoMapper;
using Terreiro.Application.Dtos;
using Terreiro.Domain.Entities;

namespace Terreiro.Application.Mappers;

internal class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<UserDto, User>().ReverseMap();
    }
}
