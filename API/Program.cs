using API.Services;
using AutoMapper;
using DogTrainer.Application;
using DogTrainer.Application.Dtos;
using DogTrainer.Application.Interfaces;
using DogTrainer.Application.Pipeline;
using DogTrainer.Application.Repositories;
using DogTrainer.Application.Skills.Commands;
using DogTrainer.Domain;
using DogTrainer.Persistance;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(AddSkillCommand.Handler).Assembly));
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));

//builder.Services.AddScoped<TokenService>();
builder.Services.AddIdentityServices(builder.Configuration);

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });
    //c.AddSecurityDefinition("token", new OpenApiSecurityScheme
    //{
    //    Type = SecuritySchemeType.ApiKey,
    //    In = ParameterLocation.Header,
    //    Name = HeaderNames.Authorization,
    //    Scheme = "Bearer"
    //});
    //c.AddSecurityRequirement(new OpenApiSecurityRequirement {
    //               {
    //                 new OpenApiSecurityScheme
    //                 {
    //                   Reference = new OpenApiReference
    //                   {
    //                     Type = ReferenceType.SecurityScheme,
    //                     Id = "Bearer"
    //                   }
    //                  },
    //                  new string[] { }
    //                }
    //            });
});


builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

//builder.Services.AddIdentityCore<AppUser>(opt =>
//{
//    opt.Password.RequireNonAlphanumeric = false;
//})
//            .AddEntityFrameworkStores<DataContext>()
//            .AddSignInManager<SignInManager<AppUser>>();

builder.Services.AddScoped<IAppUserRepository, AppUserRepository>();
builder.Services.AddScoped<ISkillRepository, SkillRepository>();
builder.Services.AddScoped<IAppUserSkillRepository, AppUserSkillRepository>();


// or more typically, using IServiceCollection
builder.Services.AddAutoMapper((sp, cfg) =>
{
    cfg.AddProfile<MappingProfiles>();
}, typeof(MappingProfiles).Assembly);


var app = builder.Build();



// Configure the HTTP request pipeline.
if(app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1"));
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
