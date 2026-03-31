using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using TaskManager.application.DTOs;
using TaskManager.Domain.Entities;

namespace TaskManager.application.Tasks.Commands
{
    public class AddNewTaskCommand : IRequest<AddTaskResponse>
    {

       
        public int? id { get; set; }

        public bool? isDeleted { get; set; }

       
        public string? Title { get; set; }

    
        public string? Description { get; set; }

        public string? userId { get; set; }

        public bool? isCompleted { get; set; }

        public string? statuse { get; set; }

        public AddNewTaskCommand() { }


    }


    public class AddTaskResponse
    {
       public int TaskID {  get; set; }

        public TaskDto Task { get; set; }

        public string Message { get; set; }

        public bool Success { get; set; }


    }



}
