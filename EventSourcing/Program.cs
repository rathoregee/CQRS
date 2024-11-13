using EventSourcing.Database;
using EventSourcing.Features.CreatePlayer;
using EventSourcing.Features.GetPlayerById;
using EventSourcing.Repository;
using EventSourcing.Validation;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<PlayerContext>(options => options.UseSqlServer("Server=.\\SQLEXPRESS;Database=PlayerDB;User Id=sa;Password=Kamran123@;TrustServerCertificate=True;"));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMediatR( configuration =>
{
    configuration.RegisterServicesFromAssemblies(typeof(Program).Assembly);
 });

builder.Services.AddValidatorsFromAssemblyContaining<CreatePlayerValidator>();
builder.Services.AddScoped<IValidator<CreatePlayerCommand>, CreatePlayerValidator>();
builder.Services.AddScoped<IRepository, Repository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
