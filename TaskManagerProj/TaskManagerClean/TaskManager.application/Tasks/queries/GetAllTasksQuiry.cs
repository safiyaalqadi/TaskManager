using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.application.DTOs;
using TaskManager.Domain.Entities;

namespace TaskManager.application.Tasks.queries
{
    public class GetAllTasksQuery : IRequest<GetAllTasksResponse>
    {
      //  public int? Id { get; set; }   we dont use it in the get all 


        public bool? IsCompleted { get; set; }

        public bool? IsDeleted { get; set; }

        public string? userId { get; set; } 

        public DateTime? CreatedDate { get; set; }


        public int pageSize { get; set; } = int.MaxValue;

        public int pageNumber { get; set; } = 1;

 

    }

    public class GetAllTasksResponse
    {
        public List<TaskDto> Tasks { get; set; }

        public int ListSize { get; set; }

        public int TasksCount { get; set; }



    }


}
