using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Terreiro.Application.Dtos;
using Terreiro.Application.Helpers;
using Terreiro.Application.Repositories;
using Terreiro.Application.Requests;
using Terreiro.Application.Resources;
using Terreiro.Domain.Entities;

namespace Terreiro.Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EventController(IEventRepository eventRepository, IMapper mapper) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] DateTime? startDate, [FromQuery] DateTime? endDate)
    {
        var events = await eventRepository.Get(startDate, endDate);
        return Ok(mapper.Map<EventDto[]>(events));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var @event = await eventRepository.Get(id, e => e.Users.Where(u => !u.DeletedAt.HasValue));
        return @event is null ?
            NotFound(TerreiroResource.EVENT_NOT_FOUND_ID.InsertParams(id)) :
            Ok(mapper.Map<EventDetailsDto>(@event));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateEventRequest request)
    {
        var items = mapper.Map<EventItem[]>(request.Items);
        var @event = new Event(request.Name, request.Period, items, request.Description);

        var rowsAffected = await eventRepository.Add(@event);
        return rowsAffected is 0 ? UnprocessableEntity(TerreiroResource.DATA_ERROR) : Created();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var @event = await eventRepository.Get(id);
        if (@event is null)
            return NotFound(TerreiroResource.EVENT_NOT_FOUND_ID.InsertParams(id));

        var rowsAffected = await eventRepository.Delete(@event);
        return rowsAffected is 0 ? UnprocessableEntity(TerreiroResource.DATA_ERROR) : NoContent();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateEventRequest request)
    {
        var @event = await eventRepository.Get(id);
        if (@event is null)
            return NotFound(TerreiroResource.EVENT_NOT_FOUND_ID.InsertParams(id));

        @event.Update(request.Name, request.Period, request.Description);

        var rowsAffected = await eventRepository.Update(@event);
        return rowsAffected is 0 ?
            UnprocessableEntity(TerreiroResource.DATA_ERROR) :
            Ok(mapper.Map<EventDto>(@event));
    }
}
