using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.application.DTOs;
using TaskManager.application.Interfaces;
using TaskManager.application.Tasks.Commands;
using TaskManager.Domain.Entities;
using TaskManagerClean.API.Hubs;

namespace TaskManager.application.Tasks.handler.TasksHandlers
{
    public class AddNewTaskHandler : IRequestHandler<AddNewTaskCommand, AddTaskResponse>
    {
        private readonly IDBContext _context;
        private readonly IMapper _mapper;
        private readonly IHubContext<UserHub> _hub;


        public AddNewTaskHandler(IDBContext context,IMapper mapper, IHubContext<UserHub> hub)
        {
            _context = context;
            _mapper = mapper;
            _hub = hub;

        }

        public async Task<AddTaskResponse> Handle(AddNewTaskCommand command, CancellationToken cancellationToken)
        {
            var taskResponse = new AddTaskResponse();

            if (command.userId ==null)
            {

                taskResponse.Message = $"user id not found  : {command.id} ";
                taskResponse.Success = false;
                return taskResponse;
            }


            var user = await _context.Users.FindAsync(command.userId);



            if (user == null)
            {

                taskResponse.Message = $"user id not found  :{command.id} ";
                taskResponse.Success = false;
                return taskResponse;

            }

            if (command.id!=null&&command.id>0&&  command.isDeleted == true)
            {
               var task = await _context.Tasks.FindAsync(command.id);
                if(task!=null)
                {
                    task.IsDeleted = true;
                    taskResponse.TaskID = task.Id;

                

                    await _context.SaveChangesAsync(cancellationToken);

                    taskResponse.Message = $"task with id :{task.Id} was deleted ";
                    taskResponse.Success = true;

                }

                else
                {
                    taskResponse.Message = $"task with id :{command.id} not found to delete ";
                    taskResponse.Success = false;
                }
              


            }
            else if (command.id != null && command.id > 0)
            {

                var task = await _context.Tasks.FindAsync(command.id);



                if (task!=null)
                {
                    var res = _mapper.Map(command, task);
                    await _context.SaveChangesAsync(cancellationToken);

                    task.UpdatedDate = DateTime.Now;

                    taskResponse.TaskID = res.Id;
                    taskResponse.Message = $"task with id :{task.Id} was updated";
                    taskResponse.Success = true;
                }
                else
                {
                    taskResponse.Message = $"task with id :{command.id} was not found to update";
                    taskResponse.Success = false;
                }
               

            }

            else
            {

                var task = new TaskItem();
                

                
                    
                  
                
                var res = _mapper.Map(command, task);


                var added = _context.Tasks.Add(res);
                await _context.SaveChangesAsync(cancellationToken);

                var newT = new TaskDto();

                newT = _mapper.Map(task, newT);
                taskResponse.TaskID = res.Id;
                taskResponse.Task = newT;
                taskResponse.Message = $"task with id :{res.Id} was added";
                taskResponse.Success = true;
                await _hub.Clients.All.SendAsync("ReceiveNewTask",task);
            }
           

          

            return taskResponse;

        }
    }
}
