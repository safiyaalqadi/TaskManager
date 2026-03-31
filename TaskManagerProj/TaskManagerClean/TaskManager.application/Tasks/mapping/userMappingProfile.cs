using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.application.DTOs;
using TaskManager.application.Tasks.Commands;
using TaskManager.application.Users.commands;
using TaskManager.Domain.Entities;

namespace TaskManager.application.Tasks.mapping
{
    public class userMappingProfile : Profile
    {
        public userMappingProfile() {
            CreateMap<signupCommand, UserItem>();
            CreateMap<UserItem,UserDto>();
            CreateMap<AddUserCommand, UserItem>();

          



        }

       
    }
}
