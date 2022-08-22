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
    public class UserRolesController : ControllerBase
    {
        private readonly IUserRoleService userRoleService;

        public UserRolesController(IUserRoleService userRoleService)
        {
            this.userRoleService = userRoleService;
        }

        [HttpGet]
        public async Task<ActionResult<List<UserRoleDto>>> Get()
        {
            ServiceResult<List<UserRoleDto>> getAllResult = await userRoleService.GetAllAsync();

            if (!getAllResult.Ok)
            {
                return Problem(getAllResult.Message);
            }

            return getAllResult.Extras;
        }

        [HttpPost]
        public async Task<ActionResult<UserRoleDto>> Post(CreateUserRoleDto createUserRoleDto)
        {
            ServiceResult<UserRoleDto> addResult = await userRoleService.AddAsync(createUserRoleDto);

            if (!addResult.Ok)
            {
                return Problem(addResult.Message);
            }

            return CreatedAtAction(nameof(Get), new { id = addResult.Extras.Id }, addResult.Extras);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            ServiceResult deleteResult = await userRoleService.DeleteAsync(id);

            if (!deleteResult.Ok)
            {
                return Problem(deleteResult.Message);
            }

            return Ok();
        }
    }
}