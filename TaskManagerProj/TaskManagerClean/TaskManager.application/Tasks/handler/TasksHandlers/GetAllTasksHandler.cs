using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TaskManager.application.DTOs;
using TaskManager.application.Interfaces;
using TaskManager.application.Tasks.queries;
using TaskManager.Domain.Entities;
using static System.Net.Mime.MediaTypeNames;

namespace TaskManager.application.Tasks.handler.TasksHandlers
{
    public class GetAllTasksHandler : IRequestHandler<GetAllTasksQuery  , GetAllTasksResponse >
    {
        private readonly IDBContext _context;
        private readonly IMapper _mapper;

        public GetAllTasksHandler(IDBContext taskRepository,IMapper mapper)
        {
            _context = taskRepository;
            _mapper = mapper;

        }
        public async Task<GetAllTasksResponse> Handle(GetAllTasksQuery request, CancellationToken cancellationToken)
        {

            var vm = new GetAllTasksResponse();

            Expression<Func<TaskItem, bool>> CompletedCondition = t => true;
            Expression<Func<TaskItem, bool>> UserCondition = t => true;
            Expression<Func<TaskItem, bool>> DateCondition = t => true;
            Expression<Func<TaskItem, bool>> IdCndition = t => true; // We don't use that cuz in the get by id case we get more complex data from other tables too or many fields from it self 
            Expression<Func<TaskItem, bool>> IsDeletedCndition = t => t.IsDeleted !=true;


            if (request.IsCompleted != null)
            {
                CompletedCondition = (t => t.IsCompleted == request.IsCompleted);
            }
            if(request.IsDeleted??false)
            {
                IsDeletedCndition = (t => t.IsDeleted == request.IsDeleted);
            }

            if (request.userId !=null)
            {
                UserCondition = (t => t.userId == request.userId);
            }
           /* if (request.Id > 0)
            {
                IdCndition = (t => t.Id == request.Id);
            }*/

            if (request.CreatedDate != null)
            {
                DateCondition = t => t.CreatedDate.Value.Date == request.CreatedDate.Value.Date;
            }


            var filteredTasks = _context.Tasks
                // .Where(IdCndition)
                 .Where(CompletedCondition)
                .Where(UserCondition)
                .Where(DateCondition)
                .Where(IsDeletedCndition);
 

            vm.Tasks = await _mapper.ProjectTo<TaskDto>(filteredTasks
                .Skip((request.pageNumber - 1) * request.pageSize)
                .Take(request.pageSize))
                        .ToListAsync();

            vm.TasksCount = filteredTasks.Count();

            return vm;

        }
    }
}
