﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Terreiro.Application.Dtos;
using Terreiro.Application.Enums;
using Terreiro.Application.Helpers;
using Terreiro.Application.Repositories;
using Terreiro.Application.Requests;
using Terreiro.Application.Resources;
using Terreiro.Domain.Entities;
using Terreiro.Presentation.Attributes;

namespace Terreiro.Presentation.Controllers;

[Authorize]
[Route("api/event")]
[ApiController]
public class EventController(IEventRepository eventRepository, IMapper mapper) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] GetEventRequest request)
    {
        var events = await eventRepository.Get(request.StartDate, request.EndDate);
        return Ok(mapper.Map<EventDto[]>(events));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var @event = await eventRepository.GetFirst(
            id,
            e => e.Users.Where(u => !u.DeletedAt.HasValue),
            e => e.Items.Where(ei => !ei.DeletedAt.HasValue)
        );

        return @event is null ?
            NotFound(TerreiroResource.EVENT_NOT_FOUND_ID.InsertParams(id)) :
            Ok(mapper.Map<EventDetailsDto>(@event));
    }

    [AuthorizeRoles(EUserRole.Admin)]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateEventRequest request)
    {
        request.Items ??= [];
        var items = mapper.Map<EventItem[]>(request.Items);
        var @event = new Event(request.Name, request.Period, items, request.Description);

        var rowsAffected = await eventRepository.Add(@event);
        return rowsAffected is 0 ?
            UnprocessableEntity(TerreiroResource.DATA_ERROR) :
            Created("", mapper.Map<EventDto>(@event));
    }

    [AuthorizeRoles(EUserRole.Admin)]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var @event = await eventRepository.GetFirst(id);
        if (@event is null)
            return NotFound(TerreiroResource.EVENT_NOT_FOUND_ID.InsertParams(id));

        @event.SetDeletedAt();

        var rowsAffected = await eventRepository.Delete(@event);
        return rowsAffected is 0 ? UnprocessableEntity(TerreiroResource.DATA_ERROR) : NoContent();
    }

    [AuthorizeRoles(EUserRole.Admin)]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateEventRequest request)
    {
        var @event = await eventRepository.GetFirst(id);
        if (@event is null)
            return NotFound(TerreiroResource.EVENT_NOT_FOUND_ID.InsertParams(id));

        @event.Update(request.Name, request.Period, request.Description);

        var rowsAffected = await eventRepository.Update(@event);
        return rowsAffected is 0 ?
            UnprocessableEntity(TerreiroResource.DATA_ERROR) :
            Ok(mapper.Map<EventDto>(@event));
    }
}
