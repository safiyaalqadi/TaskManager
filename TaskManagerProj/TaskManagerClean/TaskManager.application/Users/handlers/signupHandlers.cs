using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.application.Interfaces;
using TaskManager.application.Users.commands;
using TaskManager.Domain.Entities;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace TaskManager.application.Users.handlers
{
    public class signupHandlers: IRequestHandler<signupCommand,signupResponse>
    {
        private readonly IDBContext _context;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;
        private readonly UserManager<UserItem> _userManager;
        public signupHandlers(IDBContext context,IMapper mapper,ITokenService token,UserManager<UserItem> userManager)
        {
            _context = context;
            _mapper = mapper;
            _tokenService = token;
            _userManager = userManager;
        }
        public async Task<signupResponse> Handle(signupCommand request, CancellationToken cancellationToken)
        {
            var Response = new signupResponse();
            try{
               

                if (!string.IsNullOrEmpty(request.email))
                {
                    var existingEmailUser = await _context.Users.Where(u => u.Email == request.email).FirstOrDefaultAsync();

                    if (existingEmailUser != null)
                    {
                        Response.user = existingEmailUser;
                        Response.message = $"The user email already exists in system: {existingEmailUser.Email}";
                        Response.statuse = "failed";
                        return Response;
                    }
                }


                if (!string.IsNullOrEmpty(request.username))
                {
                    var existingUsernameUser = await _context.Users
                        .SingleOrDefaultAsync(u => u.UserName == request.username);

                    if (existingUsernameUser != null)
                    {
                        Response.user = existingUsernameUser;
                        Response.message = $"The username already exists in system: {existingUsernameUser.UserName}";
                        Response.statuse = "failed";
                        return Response;
                    }
                }


                var user = new UserItem();
                _mapper.Map(request, user);
                user.Created = DateTime.Now;
                user.Updated = DateTime.Now;

                //var res =  _context.Users.Add(user);
                var result = await _userManager.CreateAsync(user, request.password);
                if (result != null)
                {
                    var roleResult = await _userManager.AddToRoleAsync(user, user.role);
                }
                await _context.SaveChangesAsync(cancellationToken);

                List<string> Roles = new List<string>();
                Roles.Add(user.role);

                Response.user = user;
                Response.Token =  _tokenService.CreateToken(user.UserName,user.PasswordHash, user.Id, Roles);

                Response.message = $"New user sighning up to system{user.Name}";
                Response.refreshToken = _tokenService.generateRefreshToken();
                _tokenService.SaveRefreshToken(user.Id, Response.refreshToken);
                Response.statuse = "succes";


                await _context.SaveChangesAsync(cancellationToken);


                return Response;
            }
            catch (Exception ex)
            {
                Response.message += ex.ToString();
                Response.statuse = "Faild";
                return Response;
            }

          


        }
    }
}
