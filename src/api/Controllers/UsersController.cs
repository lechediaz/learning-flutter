using System.Collections.Generic;
using System.Threading.Tasks;
using api.Models;
using api.Services.Users;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IGetAllUsersService getAllUsersService;

        public UsersController(IGetAllUsersService getAllUsersService)
        {
            this.getAllUsersService = getAllUsersService;
        }

        [HttpGet("list-all")]
        public async Task<ActionResult<List<User>>> GetAll()
        {
            List<User> users = await getAllUsersService.GetAllAsync();

            return users;
        }
    }
}