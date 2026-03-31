using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.application.DTOs;

namespace TaskManager.application.Tasks.queries
{
    public class GetOneTaskQurey : IRequest<GetOneTaskResponse>
    {
        public int Id { get; set; }
    }

    public class GetOneTaskResponse
    {
        public TaskDto Task { get; set; }

       



    }
}
