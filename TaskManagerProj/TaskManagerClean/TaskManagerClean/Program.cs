using TaskManager.application.Interfaces;

using TaskManager.DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using MediatR;
using TaskManager.application.Tasks.handler.TasksHandlers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using TaskManager.application.Tasks.queries;
using TaskManager.application.Tasks.mapping;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using TaskManager.application.services;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;
using TaskManager.Domain.Entities;
using TaskManagerClean.API.Initialization;
using TaskManagerClean.API.Hubs;






var builder = WebApplication.CreateBuilder(args);



// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSignalR();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

/*builder.Services.AddScoped<IDBContext>(providor =>
providor.GetRequiredService<AppDbContext>());*/

//builder.Services.AddMediatR(cfg =>
//   cfg.RegisterServicesFromAssembly(typeof(TaskProfile).Assembly));

builder.Services.AddMediatR(cfg =>
{
    cfg.AsScoped();
}, typeof(TaskProfile).Assembly);

builder.Services.AddAutoMapper(typeof(TaskProfile).Assembly);




builder.Services.AddScoped<IDBContext, AppDbContext>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddIdentity<UserItem, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddCors(p => p.AddPolicy("TaskManger", builder =>
{
    builder.AllowAnyMethod().AllowAnyHeader().AllowCredentials()
    .WithOrigins("http://localhost:4200");
}));


builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
          .AddJwtBearer(options =>
          {
              options.TokenValidationParameters = new TokenValidationParameters
              {
                  ValidateIssuer = true,
                  ValidateAudience = true,
                  ValidateLifetime = true,
                  ValidateIssuerSigningKey = true,
                  ValidIssuer = builder.Configuration["Jwt:Issuer"],
                  ValidAudience = builder.Configuration["Jwt:Issuer"],
                  IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
              };

            
              options.Events = new JwtBearerEvents
              {
                  OnMessageReceived = context =>
                  {
                      var accessToken = context.Request.Query["Authorization"].ToString();

                     
                      var path = context.HttpContext.Request.Path;

                      if (!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments("/chat"))
                      {
                          accessToken = accessToken.Replace("Bearer ", "");

                          context.Token = accessToken;
                      }
                      return Task.CompletedTask;
                  },
                  OnAuthenticationFailed = context =>
                  {
                      
                      Console.WriteLine("Authentication failed: " + context.Exception.Message);
                      return Task.CompletedTask;
                  }
              };
          });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Customer", policy => policy.RequireRole("Customer"));
    options.AddPolicy("Admin", policy => policy.RequireRole("Admin"));

});





var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

app.MapHub<UserHub>("/taskHub");

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await RolesInitialization.SeedRoles(services);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.UseHttpsRedirection();

app.UseCors("TaskManger");

app.UseAuthorization();

app.MapControllers();




app.Run();
