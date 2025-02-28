using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Terreiro.Application.Dtos;
using Terreiro.Application.Helpers;
using Terreiro.Application.Repositories;
using Terreiro.Application.Requests;
using Terreiro.Application.Resources;
using Terreiro.Application.Services.SetPin;
using Terreiro.Domain.Entities;

namespace Terreiro.Presentation.Controllers;

[Route("api/user")]
[ApiController]
public class UserController(
    IUserRepository userRepository,
    IRoleRepository roleRepository,
    ISetPinService setPinService,
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
        var user = await userRepository.Get(id, u => u.Roles.Where(r => !r.DeletedAt.HasValue));
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
        var user = await userRepository.Get(id);
        if (user is null) return NotFound(TerreiroResource.USER_NOT_FOUND_ID.InsertParams(id));

        var rowsAffected = await userRepository.Delete(user);
        return rowsAffected is 0 ? UnprocessableEntity(TerreiroResource.DATA_ERROR) : NoContent();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpsertUserRequest request)
    {
        var user = await userRepository.Get(id);
        if (user is null) return NotFound(TerreiroResource.USER_NOT_FOUND_ID.InsertParams(id));

        user.Update(request.Name, request.CPF, request.Cellphone);

        var rowsAffected = await userRepository.Update(user);
        return rowsAffected is 0 ?
            UnprocessableEntity(TerreiroResource.DATA_ERROR) :
            Ok(mapper.Map<UserDto>(user));
    }

    [HttpPatch("{id}/pin")]
    public async Task<IActionResult> PatchPin(int id, [FromBody] PatchPinRequest request)
    {
        var user = await userRepository.Get(id);
        if (user is null) return NotFound(TerreiroResource.USER_NOT_FOUND_ID.InsertParams(id));

        setPinService.SetPin(user, request.OldPin, request.NewPin);

        var rowsAffected = await userRepository.UpdatePin(user);
        return rowsAffected is 0 ? UnprocessableEntity(TerreiroResource.DATA_ERROR) : Ok(request.NewPin);
    }

    [HttpPatch("{id}/roles")]
    public async Task<IActionResult> CreateRoles(int id, [FromBody] IEnumerable<int> roleIds)
    {
        roleIds = roleIds.Distinct();
        var user = await userRepository.Get(id, u => u.Roles);
        if (user is null) return NotFound(TerreiroResource.USER_NOT_FOUND_ID.InsertParams(id));

        var roles = await roleRepository.Get(roleIds);
        var nonExistentRoles = roleIds.Where(x => !roles.Any(y => y.Id == x));
        if (nonExistentRoles.Any())
            return NotFound(TerreiroResource.ROLE_NOT_FOUND_ID.InsertParams(string.Join(", ", nonExistentRoles)));

        user.UpdateRoles(roles);

        var rowsAffected = await userRepository.UpdateRoles(user);
        return rowsAffected is 0 ? UnprocessableEntity(TerreiroResource.DATA_ERROR) : Created();
    }
}
