using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Terreiro.Application.Dtos;
using Terreiro.Application.Enums;
using Terreiro.Application.Helpers;
using Terreiro.Application.Repositories;
using Terreiro.Application.Requests;
using Terreiro.Application.Resources;
using Terreiro.Application.Services.SetPin;
using Terreiro.Application.Services.UpdateUserEvent;
using Terreiro.Application.Services.UpdateUserEventItem;
using Terreiro.Application.Services.UpdateUserRole;
using Terreiro.Domain.Entities;
using Terreiro.Presentation.Attributes;

namespace Terreiro.Presentation.Controllers;

[Authorize]
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
    IUpdateUserRoleService updateUserRoleService,
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

    [AuthorizeRoles(EUserRole.Admin)]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] UpsertUserRequest request)
    {
        var user = new User(request.Name, request.CPF, request.Cellphone);
        var rowsAffected = await userRepository.Add(user);
        return rowsAffected is 0 ? UnprocessableEntity(TerreiroResource.DATA_ERROR) : Created();
    }

    [AuthorizeRoles(EUserRole.Admin)]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var user = await userRepository.GetFirst(id);
        if (user is null)
            return NotFound(TerreiroResource.USER_NOT_FOUND_ID.InsertParams(id));

        user.SetDeletedAt();

        var rowsAffected = await userRepository.Delete(user);
        return rowsAffected is 0 ? UnprocessableEntity(TerreiroResource.DATA_ERROR) : NoContent();
    }

    [AuthorizeRoles(EUserRole.Admin)]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpsertUserRequest request)
    {
        var user = await userRepository.GetFirst(id);
        if (user is null)
            return NotFound(TerreiroResource.USER_NOT_FOUND_ID.InsertParams(id));

        user.Update(request.Name, request.CPF, request.Cellphone);

        var rowsAffected = await userRepository.Update(user);
        return rowsAffected is 0 ?
            UnprocessableEntity(TerreiroResource.DATA_ERROR) :
            Ok(mapper.Map<UserDto>(user));
    }

    [AuthorizeRoles(EUserRole.Admin)]
    [HttpPatch("{id}/pin")]
    public async Task<IActionResult> UpdatePin(int id, [FromBody] PatchPinRequest request)
    {
        var user = await userRepository.GetFirst(id);
        if (user is null)
            return NotFound(TerreiroResource.USER_NOT_FOUND_ID.InsertParams(id));

        setPinService.SetPin(user, request.OldPin, request.NewPin);

        var rowsAffected = await userRepository.UpdatePin(user);
        return rowsAffected is 0 ? UnprocessableEntity(TerreiroResource.DATA_ERROR) : Ok(request.NewPin);
    }

    [AuthorizeRoles(EUserRole.Admin)]
    [HttpPatch("{id}/role")]
    public async Task<IActionResult> UpdateRoles(int id, [FromBody] int roleId)
    {
        var user = await userRepository.GetFirst(id, u => u.Roles.Where(r => !r.DeletedAt.HasValue));
        if (user is null)
            return NotFound(TerreiroResource.USER_NOT_FOUND_ID.InsertParams(id));

        var role = await roleRepository.GetFirst(roleId);
        if (role is null)
            return NotFound(TerreiroResource.ROLE_NOT_FOUND_ID.InsertParams(roleId));

        var (rowsAffected, updatedRole) = await updateUserRoleService.Update(user, role);

        return rowsAffected is 0 ?
            UnprocessableEntity(TerreiroResource.DATA_ERROR) :
            Ok(mapper.Map<RoleDto>(role));
    }

    [HttpPatch("event/{eventId}")]
    public async Task<IActionResult> UpdateEvent(int eventId)
    {
        var id = int.Parse(User.FindFirst("id")!.Value);
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

    [HttpPatch("event-item/{eventItemId}")]
    public async Task<IActionResult> UpdateEventItem(int eventItemId)
    {
        var id = int.Parse(User.FindFirst("id")!.Value);
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
