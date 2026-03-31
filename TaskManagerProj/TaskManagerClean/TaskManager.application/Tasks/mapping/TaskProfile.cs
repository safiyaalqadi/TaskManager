using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.application.DTOs;
using TaskManager.application.Tasks.Commands;
using TaskManager.Domain.Entities;

namespace TaskManager.application.Tasks.mapping
{
    public class TaskProfile : Profile
    {
      
            public TaskProfile()
            {
                CreateMap<TaskItem, TaskDto>();

            CreateMap<AddNewTaskCommand, TaskItem>()
           .ForMember(dest => dest.Id, opt => opt.Ignore())
           .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
           .ForAllMembers(opts =>
               opts.Condition((src, dest, srcMember) => srcMember != null));
        }
        
    }
}
