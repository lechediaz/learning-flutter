using System.Collections.Generic;
using System.Threading.Tasks;
using api.Models;
using api.Services;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService getAllUsersService;

        public UsersController(IUserService getAllUsersService)
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