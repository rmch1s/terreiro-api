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
public class RoleController(IRoleRepository roleRepository, IMapper mapper) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var roles = await roleRepository.Get();
        return Ok(mapper.Map<RoleDto[]>(roles));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var role = await roleRepository.Get(id);
        return role is null ?
            NotFound(TerreiroResource.ROLE_NOT_FOUND_ID.InsertParams(id)) :
            Ok(mapper.Map<RoleDetailsDto>(role));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] UpsertRoleRequest request)
    {
        var role = new Role(request.Name, request.Description);
        var rowsAffected = await roleRepository.Add(role);
        return rowsAffected is 0 ? UnprocessableEntity(TerreiroResource.DATA_ERROR) : Created();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var role = await roleRepository.Get(id);
        if (role is null)
            return NotFound(TerreiroResource.ROLE_NOT_FOUND_ID.InsertParams(id));

        var rowsAffected = await roleRepository.Delete(role);
        return rowsAffected is 0 ? UnprocessableEntity(TerreiroResource.DATA_ERROR) : NoContent();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpsertRoleRequest request)
    {
        var role = await roleRepository.Get(id);
        if (role is null)
            return NotFound(TerreiroResource.ROLE_NOT_FOUND_ID.InsertParams(id));

        role.Update(request.Name, request.Description);

        var rowsAffected = await roleRepository.Update(role);
        return rowsAffected is 0 ?
            UnprocessableEntity(TerreiroResource.DATA_ERROR) :
            Ok(mapper.Map<RoleDetailsDto>(role));
    }
}