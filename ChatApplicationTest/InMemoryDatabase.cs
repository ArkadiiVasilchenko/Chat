using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using ChatApplication.Data;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.DependencyInjection;
using ChatApplication.Models;

namespace ChatApplicationTest
{
    public class InMemoryDatabase
    {
        private WebApplicationFactory<Program> _factory;

        public InMemoryDatabase()
        {
            _factory = new WebApplicationFactory<Program>().WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    services.RemoveAll(typeof(DbContextOptions<AppDbContext>));
                    services.AddDbContext<AppDbContext>(options =>
                    {
                        options.UseInMemoryDatabase("test");
                    });
                });
            });
        }

        [Fact]
        public async Task OnGetUsers_WhenExecuteApi()
        {
            //Arrange
            using (var scope = _factory.Services.CreateScope())
            {
                var scopeServices = scope.ServiceProvider;
                var dbContext = scopeServices.GetRequiredService<AppDbContext>();

                //dbContext.Database.EnsureDeleted();
                dbContext.Database.EnsureCreated();
                dbContext.User.Add(new User()
                {
                    Name = "User1"
                });
                dbContext.SaveChanges();
            }

            var client = _factory.CreateClient();

            //Act
            var response = await client.GetAsync("/api/User/GetUsers");

            var result = await response.Content.ReadFromJsonAsync<List<User>>();

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
