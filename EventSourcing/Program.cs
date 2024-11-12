using EventSourcing.Features.CreatePlayer;
using EventSourcing.Features.GetPlayerById;
using EventSourcing.Validation;
using FluentValidation;
using FluentValidation.AspNetCore;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMediatR( configuration =>
{
    configuration.RegisterServicesFromAssemblies(typeof(Program).Assembly);
 });

builder.Services.AddValidatorsFromAssemblyContaining<CreatePlayerValidator>();
builder.Services.AddScoped<IValidator<CreatePlayerCommand>, CreatePlayerValidator>();

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
