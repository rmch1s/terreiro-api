using AutoMapper;
using Terreiro.Application.Dtos;
using Terreiro.Application.Requests;
using Terreiro.Domain.Entities;

namespace Terreiro.Application.Mappers;

internal class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<UserDto, User>().ReverseMap();
        CreateMap<UserDetailsDto, User>().ReverseMap();

        CreateMap<RoleDto, Role>().ReverseMap();

        CreateMap<EventDto, Event>().ReverseMap();
        CreateMap<EventDetailsDto, Event>().ReverseMap();

        CreateMap<UpsertEventItemRequest, EventItem>().ReverseMap();
    }
}
