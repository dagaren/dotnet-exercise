// <copyright file="DbContextRespository.cs" company="dagaren">
// Copyright (c) dagaren. All rights reserved.
// </copyright>

namespace Example.API.DataAccess
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using AutoMapper;
    using Example.API.Domain.Model;
    using Example.API.Domain.Repositories;
    using Microsoft.EntityFrameworkCore;

    public class DbContextRespository : IUserRepository
    {
        private readonly UserDbContext dbContext;

        private readonly IMapper mapper;

        public DbContextRespository(UserDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));

            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<Domain.Model.User> Create(UserInfo userInfo)
        {
            return await this.SafeExecution(
               async () =>
               {
                   var user = this.mapper.Map<User>(userInfo);

                   this.dbContext.Users.Add(user);

                   await this.dbContext.SaveChangesAsync();

                   return this.mapper.Map<Domain.Model.User>(user);
               },
               "Error creating user");
        }

        public async Task DeleteById(int id)
        {
            var entityToRemove = await this.GetUserById(id);

            await this.SafeExecution(
               async () =>
               {
                   this.dbContext.Users.Remove(entityToRemove);
                   await this.dbContext.SaveChangesAsync();
               },
               "Error removing user");
        }

        public async Task<IEnumerable<Domain.Model.User>> GetAll()
        {
            return await this.SafeExecution(
                async () =>
                {
                    var users = await this.dbContext.Users.ToListAsync();

                    return this.mapper.Map<IEnumerable<Domain.Model.User>>(users);
                },
                "Error retrieving users");
        }

        public async Task<Domain.Model.User> GetById(int id)
        {
            User entityUser = await this.GetUserById(id);

            return this.mapper.Map<Domain.Model.User>(entityUser);
        }

        public async Task Update(Domain.Model.User user)
        {
            User entityUser = await this.GetUserById(user.Id);

            await this.SafeExecution(
                async () =>
                {
                    this.mapper.Map<Domain.Model.User, User>(user, entityUser);

                    this.dbContext.Update(entityUser);

                    await this.dbContext.SaveChangesAsync();
                },
                "Error updating user");
        }

        private async Task<User> GetUserById(int id)
        {
            User user = default(User);

            await this.SafeExecution(
               async () =>
               {
                   user = await this.dbContext.Users.SingleOrDefaultAsync(x => x.Id == id);
               },
               "Error retrieving user");

            return user ?? throw new EntityNotFoundException();
        }

        private async Task SafeExecution(Func<Task> action, string errorMessage)
        {
            try
            {
               await action();
            }
            catch (Exception ex)
            {
                throw new RepositoryException(errorMessage, ex);
            }
        }

        private async Task<T> SafeExecution<T>(Func<Task<T>> action, string errorMessage)
        {
            try
            {
                return await action();
            }
            catch (Exception ex)
            {
                throw new RepositoryException(errorMessage, ex);
            }
        }
    }
}
