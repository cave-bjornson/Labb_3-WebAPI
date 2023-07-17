using System.Text.Json;
using System.Text.Json.Serialization;
using kDg.FileBaseContext.Extensions;
using Microsoft.EntityFrameworkCore;
using Threenine.Data.DependencyInjection;
using WebAPI.Data;
using WebAPI.Models;
using WebAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services
    .AddDbContext<ApplicationDbContext>(optionsBuilder =>
    {
        optionsBuilder.UseSqlite("DataSource=app.db;Cache=Shared");
    })
    .AddUnitOfWork<ApplicationDbContext>();

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IPersonService, PersonService>();
builder.Services.AddScoped<IInterestService, InterestService>();
builder.Services.AddScoped<ILinkService, LinkService>();

builder.Services.AddSingleton<PersonMapper>();
builder.Services.AddSingleton<InterestMapper>();
builder.Services.AddSingleton<LinkMapper>();

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
