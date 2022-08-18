using System.Collections.Generic;
using System.Threading.Tasks;
using api.Models.Entities;
using api.Services;
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
        public async Task<ActionResult<List<User>>> GetAll()
        {
            List<User> users = await userService.GetAllAsync();

            return users;
        }
    }
}