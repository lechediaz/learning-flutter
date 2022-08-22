using System.Collections.Generic;
using System.Threading.Tasks;
using api.Models.Dtos;
using api.Services;
using api.Services.Base;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RolesController : ControllerBase
    {
        private readonly IRoleService roleService;

        public RolesController(IRoleService roleService)
        {
            this.roleService = roleService;
        }

        [HttpGet]
        public async Task<ActionResult<List<RoleDto>>> Get()
        {
            ServiceResult<List<RoleDto>> getAllResult = await roleService.GetAllAsync();

            if (!getAllResult.Ok)
            {
                return Problem(getAllResult.Message);
            }

            return getAllResult.Extras;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<RoleDto>> Get(int id)
        {
            ServiceResult<RoleDto> getByIdResult = await roleService.GetByIdAsync(id);

            if (!getByIdResult.Ok)
            {
                return Problem(getByIdResult.Message);
            }

            return getByIdResult.Extras;
        }

        [HttpPost]
        public async Task<ActionResult<RoleDto>> Post(CreateRoleDto createRoleDto)
        {
            ServiceResult<RoleDto> addResult = await roleService.AddAsync(createRoleDto);

            if (!addResult.Ok)
            {
                return Problem(addResult.Message);
            }

            return CreatedAtAction(nameof(Get), new { id = addResult.Extras.Id }, addResult.Extras);
        }

        [HttpPut]
        public async Task<ActionResult> Put(UpdateRoleDto updateRoleDto)
        {
            ServiceResult updateResult = await roleService.UpdateAsync(updateRoleDto);

            if (!updateResult.Ok)
            {
                return Problem(updateResult.Message);
            }

            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            ServiceResult deleteResult = await roleService.DeleteAsync(id);

            if (!deleteResult.Ok)
            {
                return Problem(deleteResult.Message);
            }

            return Ok();
        }
    }
}