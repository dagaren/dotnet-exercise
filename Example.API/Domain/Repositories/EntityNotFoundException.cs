// <copyright file="EntityNotFoundException.cs" company="dagaren">
// Copyright (c) dagaren. All rights reserved.
// </copyright>

namespace Example.API.Domain.Repositories
{
    using System;

    public class EntityNotFoundException<T> : Exception
    {
        public EntityNotFoundException()
        {
        }

        public EntityNotFoundException(T entityId)
            : base("Entity not found")
        {
            this.EntityId = entityId;
        }

        public T EntityId { get; }
    }
}
