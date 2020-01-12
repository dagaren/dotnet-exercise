// <copyright file="UserController.cs" company="dagaren">
// Copyright (c) dagaren. All rights reserved.
// </copyright>

namespace Example.API.Controllers
{
    using Example.API.Controllers.ViewModels;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return this.Ok(new UserResponse[]
            {
                new UserResponse
                {
                    Id = 1,
                    Name = "Name1",
                    Birthdate = "2019-01-01",
                },
                new UserResponse
                {
                    Id = 1,
                    Name = "Name2",
                    Birthdate = "2019-01-01",
                },
            });
        }

        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(int id)
        {
            return this.Ok(new UserResponse
            {
                Id = id,
                Name = "Name",
                Birthdate = "2019-01-01",
            });
        }

        [HttpPost]
        public IActionResult Post([FromBody] UserRequest user)
        {
            return this.NoContent();
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UserRequest user)
        {
            return this.NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return this.NoContent();
        }
    }
}
