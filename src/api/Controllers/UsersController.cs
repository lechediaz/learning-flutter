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
    public class UsersController : ControllerBase
    {
        private readonly IUserService userService;

        public UsersController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<List<UserDto>>> Get()
        {
            ServiceResult<List<UserDto>> getAllResult = await userService.GetAllAsync();

            if (!getAllResult.Ok)
            {
                return Problem(getAllResult.Message);
            }

            return getAllResult.Extras;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<UserDto>> Get(int id)
        {
            ServiceResult<UserDto> getByIdResult = await userService.GetByIdAsync(id);

            if (!getByIdResult.Ok)
            {
                return Problem(getByIdResult.Message);
            }

            return getByIdResult.Extras;
        }

        [HttpPost]
        public async Task<ActionResult<UserDto>> Post(CreateUserDto createUserDto)
        {
            ServiceResult<UserDto> addResult = await userService.AddAsync(createUserDto);

            if (!addResult.Ok)
            {
                return Problem(addResult.Message);
            }

            return CreatedAtAction(nameof(Get), new { id = addResult.Extras.Id }, addResult.Extras);
        }

        [HttpPut]
        public async Task<ActionResult> Put(UpdateUserDto updateUserDto)
        {
            ServiceResult updateResult = await userService.UpdateAsync(updateUserDto);

            if (!updateResult.Ok)
            {
                return Problem(updateResult.Message);
            }

            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Put(int id)
        {
            ServiceResult deleteResult = await userService.DeleteAsync(id);

            if (!deleteResult.Ok)
            {
                return Problem(deleteResult.Message);
            }

            return Ok();
        }
    }
}