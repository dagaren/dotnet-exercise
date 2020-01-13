// <copyright file="RepositoryException.cs" company="dagaren">
// Copyright (c) dagaren. All rights reserved.
// </copyright>

namespace Example.API.Domain.Repositories
{
    using System;

    public class RepositoryException : Exception
    {
        public RepositoryException()
            : base()
        {
        }

        public RepositoryException(string message)
            : base(message)
        {
        }

        public RepositoryException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
