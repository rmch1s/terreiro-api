using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Terreiro.Application.Dtos;
using Terreiro.Application.Helpers;
using Terreiro.Application.Repositories;
using Terreiro.Application.Requests;
using Terreiro.Application.Resources;
using Terreiro.Application.Services.SetPin;
using Terreiro.Application.Services.UpdateUserEvent;
using Terreiro.Application.Services.UpdateUserEventItem;
using Terreiro.Domain.Entities;

namespace Terreiro.Presentation.Controllers;

[Route("api/user")]
[ApiController]
public class UserController(
    IUserRepository userRepository,
    IRoleRepository roleRepository,
    IEventRepository eventRepository,
    IEventItemRepository eventItemRepository,
    ISetPinService setPinService,
    IUpdateUserEventService updateUserEventService,
    IUpdateUserEventItemService updateUserEventItemService,
    IMapper mapper
) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var users = await userRepository.Get();
        return Ok(mapper.Map<UserDto[]>(users));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var user = await userRepository.GetFirst(id, u => u.Roles.Where(r => !r.DeletedAt.HasValue));
        return user is null ?
            NotFound(TerreiroResource.USER_NOT_FOUND_ID.InsertParams(id)) :
            Ok(mapper.Map<UserDetailsDto>(user));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] UpsertUserRequest request)
    {
        var user = new User(request.Name, request.CPF, request.Cellphone);
        var rowsAffected = await userRepository.Add(user);
        return rowsAffected is 0 ? UnprocessableEntity(TerreiroResource.DATA_ERROR) : Created();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var user = await userRepository.GetFirst(id);
        if (user is null) return NotFound(TerreiroResource.USER_NOT_FOUND_ID.InsertParams(id));

        user.SetDeletedAt();

        var rowsAffected = await userRepository.Delete(user);
        return rowsAffected is 0 ? UnprocessableEntity(TerreiroResource.DATA_ERROR) : NoContent();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpsertUserRequest request)
    {
        var user = await userRepository.GetFirst(id);
        if (user is null) return NotFound(TerreiroResource.USER_NOT_FOUND_ID.InsertParams(id));

        user.Update(request.Name, request.CPF, request.Cellphone);

        var rowsAffected = await userRepository.Update(user);
        return rowsAffected is 0 ?
            UnprocessableEntity(TerreiroResource.DATA_ERROR) :
            Ok(mapper.Map<UserDto>(user));
    }

    [HttpPatch("{id}/pin")]
    public async Task<IActionResult> UpdatePin(int id, [FromBody] PatchPinRequest request)
    {
        var user = await userRepository.GetFirst(id);
        if (user is null) return NotFound(TerreiroResource.USER_NOT_FOUND_ID.InsertParams(id));

        setPinService.SetPin(user, request.OldPin, request.NewPin);

        var rowsAffected = await userRepository.UpdatePin(user);
        return rowsAffected is 0 ? UnprocessableEntity(TerreiroResource.DATA_ERROR) : Ok(request.NewPin);
    }

    [HttpPatch("{id}/roles")]
    public async Task<IActionResult> UpdateRoles(int id, [FromBody] IEnumerable<int> roleIds)
    {
        roleIds = roleIds.Distinct();
        var user = await userRepository.GetFirst(id, u => u.Roles);
        if (user is null) return NotFound(TerreiroResource.USER_NOT_FOUND_ID.InsertParams(id));

        var roles = await roleRepository.Get(roleIds);
        var nonExistentRoles = roleIds.Where(x => !roles.Any(y => y.Id == x));
        if (nonExistentRoles.Any())
            return NotFound(TerreiroResource.ROLE_NOT_FOUND_ID.InsertParams(string.Join(", ", nonExistentRoles)));

        user.UpdateRoles(roles);

        var rowsAffected = await userRepository.UpdateRoles(user);
        return rowsAffected is 0 ?
            UnprocessableEntity(TerreiroResource.DATA_ERROR) :
            Ok(mapper.Map<RoleDto[]>(roles));
    }

    [HttpPatch("{id}/event/{eventId}")]
    public async Task<IActionResult> UpdateEvent(int id, int eventId)
    {
        var user = await userRepository.GetFirst(id, u => u.Events.Where(e => e.Id == eventId));
        if (user is null)
            return NotFound(TerreiroResource.USER_NOT_FOUND_ID.InsertParams(id));

        var @event = await eventRepository.GetFirst(eventId);
        if (@event is null)
            return NotFound(TerreiroResource.EVENT_NOT_FOUND_ID.InsertParams(id));

        var (rowsAffected, updatedEvent) = await updateUserEventService.Update(user, @event);
        return rowsAffected is 0 ?
            UnprocessableEntity(TerreiroResource.DATA_ERROR) :
            Ok(mapper.Map<EventDto?>(updatedEvent));
    }

    [HttpPatch("{id}/event-item/{eventItemId}")]
    public async Task<IActionResult> UpdateEventItem(int id, int eventItemId)
    {
        var user = await userRepository.GetFirst(id, u => u.EventItems.Where(e => e.Id == eventItemId));
        if (user is null)
            return NotFound(TerreiroResource.USER_NOT_FOUND_ID.InsertParams(id));

        var eventItem = await eventItemRepository.GetFirst(eventItemId);
        if (eventItem is null)
            return NotFound(TerreiroResource.EVENT_NOT_FOUND_ID.InsertParams(id));

        var (rowsAffected, updatedEventItem) = await updateUserEventItemService.Update(user, eventItem);
        return rowsAffected is 0 ?
            UnprocessableEntity(TerreiroResource.DATA_ERROR) :
            Ok(mapper.Map<EventItemDto?>(updatedEventItem));
    }
}
