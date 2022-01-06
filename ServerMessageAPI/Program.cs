using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ServerMessageAPI.AutoMapper;
using ServerMessageAPI.ChannelAggregate.Port;
using ServerMessageAPI.Controllers;
using ServerMessageAPI.Models;
using ServerMessageAPI.Models.Port;
using ServerMessageAPI.UserAggregate.Port;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IMessageRepository, MessageRepository>();
builder.Services.AddScoped<IChannelRepository, ChannelRepository>();
builder.Services.AddDbContextPool<ServerMessageContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ServerMessageDb")));

var mappingConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new MappingProfile());
});

var mapper = mappingConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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