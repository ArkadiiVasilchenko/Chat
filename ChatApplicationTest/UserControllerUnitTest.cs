using ChatApplication.Controllers;
using ChatApplication.Models;
using ChatApplication.Services.ServiceInterfaces;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApplicationTest
{
    public class UserControllerUnitTest
    {
        private readonly IUserService _userService;
        private readonly UserController _controller;
        public UserControllerUnitTest()
        {
            _userService = A.Fake<IUserService>();
            _controller = new UserController(_userService);
        }

        [Fact]
        public void UserController_GetUsers_ReturnOk()
        {
            // Arrange
            var fakeUsers = new List<User>
         {
             new User { Id = 1, Name = "User1" },
             new User { Id = 2, Name = "User2" }
         };
            A.CallTo(() => _userService.Read()).Returns(fakeUsers);

            // Act
            var result = _controller.GetUsers();

            // Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var users = okResult.Value.Should().BeAssignableTo<List<User>>().Subject;

            users.Should().HaveCount(2);
            users.Should().Contain(fakeUsers);
        }
    }
}
