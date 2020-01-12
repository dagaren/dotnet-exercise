// <copyright file="IUserRepository.cs" company="dagaren">
// Copyright (c) dagaren. All rights reserved.
// </copyright>

namespace Example.API.Domain.Repositories
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Example.API.Domain.Model;

    public interface IUserRepository
    {
        Task<User> GetById(int id);

        Task<IEnumerable<User>> GetAll();

        Task<User> Create(UserInfo userInfo);

        Task Update(User user);

        Task DeleteById(int id);
    }
}
