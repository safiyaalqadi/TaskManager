using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TaskManagerProj.Entitys;

namespace TaskManagerProj.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public IActionResult Index()
        {
           private readonly UserManager<UserEntity> _userManager;
           
        }
    }
}
