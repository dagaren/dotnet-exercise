// <copyright file="UserControllerTest.cs" company="dagaren">
// Copyright (c) dagaren. All rights reserved.
// </copyright>

namespace Example.API.Tests.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using AutoMapper;
    using Example.API.Controllers;
    using Example.API.Controllers.ViewModels;
    using Example.API.Domain.Model;
    using Example.API.Domain.Repositories;
    using FluentAssertions;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using NSubstitute;

    [TestClass]
    public class UserControllerTest
    {
        private IMapper mapperMock;

        private IUserRepository userRepositoryMock;

        private UserController userControllerSut;

        [TestInitialize]
        public void Initialize()
        {
            this.mapperMock = Substitute.For<IMapper>();

            this.userRepositoryMock = Substitute.For<IUserRepository>();

            this.userControllerSut = new UserController(this.userRepositoryMock, this.mapperMock);
        }

        [TestMethod]
        public async Task GetAll_OkObjectResultReturned()
        {
            // Arrange
            this.userRepositoryMock.GetAll().Returns(new List<User>());
            this.mapperMock.Map<IEnumerable<UserResponse>>(Arg.Any<IEnumerable<Domain.Model.User>>()).Returns(new List<UserResponse>());

            // Act
            IActionResult result = await this.userControllerSut.GetAll();

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var objectResult = result as OkObjectResult;
            objectResult.Value.Should().NotBeNull().And.BeAssignableTo<IEnumerable<UserResponse>>();
        }

        [TestMethod]
        public async Task Get_ObjectForIdExists_OkObjectResultReturned()
        {
            // Arrange
            this.userRepositoryMock.GetById(Arg.Any<int>()).Returns(new User());
            this.mapperMock.Map<UserResponse>(Arg.Any<User>()).Returns(new UserResponse());

            // Act
            IActionResult result = await this.userControllerSut.Get(1);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var objectResult = result as OkObjectResult;
            objectResult.Value.Should().NotBeNull().And.BeOfType<UserResponse>();
        }

        [TestMethod]
        public async Task Post_RepositoryAcceptsObject_OkObjectResultReturned()
        {
            // Arrange
            this.userRepositoryMock.GetById(Arg.Any<int>()).Returns(new User());
            this.mapperMock.Map<UserResponse>(Arg.Any<User>()).Returns(new UserResponse());
            this.mapperMock.Map<UserInfo>(Arg.Any<UserRequest>()).Returns(new UserInfo());

            var userRequest = new UserRequest();

            // Act
            IActionResult result = await this.userControllerSut.Post(userRequest);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var objectResult = result as OkObjectResult;
            objectResult.Value.Should().NotBeNull().And.BeOfType<UserResponse>();
        }

        [TestMethod]
        public async Task Put_RepositoryAcceptsObject_NoContentResultReturned()
        {
            // Arrange
            this.mapperMock.Map<User>(Arg.Any<UserRequest>()).Returns(new User());

            var userRequest = new UserRequest();

            // Act
            IActionResult result = await this.userControllerSut.Put(1, userRequest);

            // Assert
            result.Should().BeOfType<NoContentResult>();
        }

        [TestMethod]
        public async Task Delete_RepositoryAcceptsRequest_OkObjectResultReturned()
        {
            // Arrange

            // Act
            IActionResult result = await this.userControllerSut.Delete(1);

            // Assert
            result.Should().BeOfType<NoContentResult>();
        }
    }
}
