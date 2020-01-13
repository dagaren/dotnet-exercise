// <copyright file="GlobalExceptionFilter.cs" company="dagaren">
// Copyright (c) dagaren. All rights reserved.
// </copyright>

namespace Example.API.Filters
{
    using System;
    using Example.API.Domain.Repositories;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;

    public class GlobalExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            context.Result = this.GetResult(context.Exception);
        }

        private IActionResult GetResult(Exception exception)
        {
            switch (exception)
            {
                case EntityNotFoundException _:
                    return new NotFoundObjectResult("Entity not found");
                default:
                    return new ObjectResult("Internal server error")
                    {
                        StatusCode = 500,
                    };
            }
        }
    }
}
