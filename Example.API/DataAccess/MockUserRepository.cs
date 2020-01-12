// <copyright file="MockUserRepository.cs" company="dagaren">
// Copyright (c) dagaren. All rights reserved.
// </copyright>

namespace Example.API.DataAccess
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Example.API.Domain.Model;
    using Example.API.Domain.Repositories;

    public class MockUserRepository : IUserRepository
    {
        private readonly Random random = new Random(Guid.NewGuid().GetHashCode());

        public Task<IEnumerable<User>> GetAll()
        {
            return Task.FromResult(new User[]
            {
                new User
                {
                    Id = this.random.Next(),
                    Name = "Name",
                    Birthdate = new DateTime(2019, 1, 1),
                },
                new User
                {
                    Id = this.random.Next(),
                    Name = "Name",
                    Birthdate = new DateTime(2019, 1, 1),
                },
            }.AsEnumerable());
        }

        public Task<User> GetById(int id)
        {
            return Task.FromResult(new User
            {
                Id = this.random.Next(),
                Name = "Name",
                Birthdate = new DateTime(2019, 1, 1),
            });
        }

        public Task<User> Create(UserInfo userInfo)
        {
            return Task.FromResult(new User
            {
                Id = this.random.Next(),
                Name = userInfo.Name,
                Birthdate = userInfo.Birthdate,
            });
        }

        public Task Update(User user)
        {
            return Task.CompletedTask;
        }

        public Task DeleteById(int id)
        {
            return Task.CompletedTask;
        }
    }
}
