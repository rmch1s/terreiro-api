using AutoMapper;
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
[Route("api/event-item")]
[ApiController]
[AuthorizeRoles(EUserRole.Admin)]
public class EventItemController(
    IEventItemRepository eventItemRepository,
    IEventRepository eventRepository,
    IMapper mapper
) : ControllerBase
{
    [HttpPost("event/{eventId}")]
    public async Task<IActionResult> Create(int eventId, [FromBody] UpsertEventItemRequest[] request)
    {
        var @event = await eventRepository.GetFirst(eventId);

        if (@event is null)
            return NotFound(TerreiroResource.EVENT_NOT_FOUND_ID.InsertParams(eventId));

        var eventItems = request.Select(x => new EventItem(x.Name, x.Quantity, eventId));

        var rowsAffected = await eventItemRepository.Add(eventItems);
        return rowsAffected is 0 ?
            UnprocessableEntity(TerreiroResource.DATA_ERROR) :
            Created("", mapper.Map<EventItemDto[]>(eventItems));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var eventItem = await eventItemRepository.GetFirst(id);
        if (eventItem is null)
            return NotFound(TerreiroResource.EVENT_ITEM_NOT_FOUND_ID.InsertParams(id));

        eventItem.SetDeletedAt();

        var rowsAffected = await eventItemRepository.Delete(eventItem);
        return rowsAffected is 0 ? UnprocessableEntity(TerreiroResource.DATA_ERROR) : NoContent();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpsertEventItemRequest request)
    {
        var eventItem = await eventItemRepository.GetFirst(id);
        if (eventItem is null)
            return NotFound(TerreiroResource.EVENT_ITEM_NOT_FOUND_ID.InsertParams(id));

        eventItem.Update(request.Name, request.Quantity);

        var rowsAffected = await eventItemRepository.Update(eventItem);
        return rowsAffected is 0 ?
            UnprocessableEntity(TerreiroResource.DATA_ERROR) :
            Ok(mapper.Map<EventItemDto>(eventItem));
    }
}
