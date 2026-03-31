using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using TaskManager.application.Tasks.Commands;
using TaskManager.application.Tasks.queries;
using TaskManager.DataAccess.Data;
using TaskManager.Domain.Entities;
using TaskManagerClean.API.Hubs;

namespace TaskManagerClean.API.Controllers
{
    [ApiController]
    [Route("api/tasks")]
    public class TasksController : ControllerBase
    {

        private readonly IMediator _mediator;
        private readonly IHubContext<UserHub> _hubContext;

        public TasksController(IMediator mediator,IHubContext<UserHub> hubContext)
        {
            _mediator = mediator;
            _hubContext = hubContext;
        }
        [HttpGet]
        //[Authorize]
        public async Task<IActionResult> Get([FromQuery] GetAllTasksQuery quiry)
        {

             try {
                 var result = await _mediator.Send(quiry);
                 return Ok(result);
             }
             catch (Exception ex) { 
                 return BadRequest(ex.Message);
             }
            /*var result = await _mediator.Send(quiry);
            return Ok(result);*/




        }

        [HttpGet("getByid")]
        //[Authorize]
        public  async Task<IActionResult> GetTaskById([FromQuery] GetOneTaskQurey quiry)
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
        //[Authorize]
        public async Task<IActionResult> Create([FromBody] AddNewTaskCommand command)
        {

            var result = await _mediator.Send(command);


            if(result.Success)
            {
                await _hubContext.Clients.All.SendAsync("ReceiveNewTask", $"New task added: {command.Title}");
                return Ok(result);
            
            }
            else
            {
                return BadRequest(result);
            }

        }

       




    }
}
