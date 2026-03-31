using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using TaskManager.application.Tasks.Commands;
using TaskManager.application.Tasks.queries;
using TaskManager.application.Users.commands;
using TaskManager.application.Users.queries;
using TaskManager.DataAccess.Data;
using TaskManager.Domain.Entities;

namespace TaskManagerClean.API.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UsersController(IMediator mediator)
        {
            _mediator = mediator;

        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetUsersQuery quiry)
        {

            try
            {
                var result = await _mediator.Send(quiry);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
          




        }

        [HttpGet("getByid")]
        //[Authorize]
        public async Task<IActionResult> GetTaskById([FromQuery] getoneUserQuery quiry)
        {

            try
            {
                var result = await _mediator.Send(quiry);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddUserCommand command)
        {

            var result = await _mediator.Send(command);
           
            return Ok(result);

        }





    }
}
