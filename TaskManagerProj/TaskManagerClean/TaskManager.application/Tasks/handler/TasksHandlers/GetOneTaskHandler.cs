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
    public class GetOneTaskHandler : IRequestHandler<GetOneTaskQurey, GetOneTaskResponse>
    {
        private readonly IDBContext _context;
        private readonly IMapper _mapper;

        public GetOneTaskHandler(IDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GetOneTaskResponse> Handle(GetOneTaskQurey request, CancellationToken cancellationToken)
        {
            var vm = new GetOneTaskResponse();

            Expression<Func<TaskItem, bool>> IdCndition = t => true;

            if (request.Id > 0)
            {
                IdCndition = (t => t.Id == request.Id);
            }

            var filteredTasks = _context.Tasks
                .Where(IdCndition);


            vm.Task = await _mapper.ProjectTo<TaskDto>(filteredTasks).SingleOrDefaultAsync(cancellationToken);




            return vm;

        }
    }
}
