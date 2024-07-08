using ChatApplication.Data;
using ChatApplication.Extensions;
using ChatApplication.Hubs;
using ChatApplication.Hubs.HubsInterfaces;
using ChatApplication.Repositories;
using ChatApplication.Repositories.RepositoryInterfaces;
using ChatApplication.Services;
using ChatApplication.Services.ServiceInterfaces;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

public class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        builder.Services.AddHttpContextAccessor();

        //registrate services
        builder.Services.AddCustomServices();

        builder.Services.AddCors();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddSignalR();

        builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        app.UseRouting();
        app.UseCors(builder => builder
            .WithOrigins("http://localhost:44391")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials()
        );

        app.UseHttpsRedirection();

        app.MapControllers();
        app.MapHub<NotificationHub>("/notification-hub");


        app.Run();
    }
}