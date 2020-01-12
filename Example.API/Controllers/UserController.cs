// <copyright file="UserController.cs" company="dagaren">
// Copyright (c) dagaren. All rights reserved.
// </copyright>

namespace Example.API.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using AutoMapper;
    using Example.API.Controllers.ViewModels;
    using Example.API.Domain.Model;
    using Example.API.Domain.Repositories;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository userRepository;

        private readonly IMapper mapper;

        public UserController(IUserRepository userRepository, IMapper mapper)
        {
            this.userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));

            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            IEnumerable<User> users = await this.userRepository.GetAll();

            IEnumerable<UserResponse> response = this.mapper.Map<IEnumerable<UserResponse>>(users);

            return this.Ok(response);
        }

        [HttpGet("{id}", Name = "Get")]
        public async Task<IActionResult> Get(int id)
        {
            User user = await this.userRepository.GetById(id);

            var response = this.mapper.Map<UserResponse>(user);

            return this.Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UserRequest userRequest)
        {
            var userInfo = this.mapper.Map<UserInfo>(userRequest);

            User user = await this.userRepository.Create(userInfo);

            var response = this.mapper.Map<UserResponse>(user);

            return this.Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] UserRequest userRequest)
        {
            var user = this.mapper.Map<User>(userRequest);
            user.Id = id;

            await this.userRepository.Update(user);

            return this.NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await this.userRepository.DeleteById(id);

            return this.NoContent();
        }
    }
}
