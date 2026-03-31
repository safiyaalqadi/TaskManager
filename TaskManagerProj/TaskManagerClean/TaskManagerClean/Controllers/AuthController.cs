using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaskManager.application.Interfaces;
using TaskManager.application.Tasks.Commands;
using TaskManager.application.Users.commands;

namespace TaskManagerClean.API.Controllers
{
    [ApiController]
    [Route("api/Auth")]
    public class AuthController:ControllerBase
    {

        private readonly IMediator _mediator;



        public AuthController(IMediator mediator)
          {
            _mediator = mediator;
           
          }

        [HttpPost("login")]

        public async Task<IActionResult> login([FromBody] loginCommand command)
        {

            var result = await _mediator.Send(command);
            if (result!=null)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
           

        }

        [HttpPost("signup")]
        public async Task<IActionResult> signup([FromBody] signupCommand command)
        {

            var result = await _mediator.Send(command);
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }

        }




    }
}
