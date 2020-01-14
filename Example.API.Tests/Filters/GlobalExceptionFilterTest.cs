// <copyright file="GlobalExceptionFilterTest.cs" company="dagaren">
// Copyright (c) dagaren. All rights reserved.
// </copyright>

namespace Example.API.Tests.Filters
{
    using System;
    using System.Collections.Generic;
    using Example.API.Domain.Repositories;
    using Example.API.Filters;
    using FluentAssertions;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Abstractions;
    using Microsoft.AspNetCore.Mvc.Filters;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using NSubstitute;

    [TestClass]
    public class GlobalExceptionFilterTest
    {
        [TestMethod]
        public void OnException_WithEntityNotFoundException_NotFoundReturned()
        {
            // Arrange
            var sut = new GlobalExceptionFilter();
            ExceptionContext exceptionContext = GenerateExceptionContext(new EntityNotFoundException());

            // Act
            sut.OnException(exceptionContext);

            // Assert
            exceptionContext.Result.Should().BeOfType<NotFoundObjectResult>();
        }

        [TestMethod]
        public void OnException_WithGenericException_ObjectResultWith500StatusCodeReturned()
        {
            // Arrange
            var sut = new GlobalExceptionFilter();
            ExceptionContext exceptionContext = GenerateExceptionContext(new Exception());

            // Act
            sut.OnException(exceptionContext);

            // Assert
            exceptionContext.Result.Should().BeOfType<ObjectResult>();
            var objectResult = exceptionContext.Result as ObjectResult;
            objectResult.StatusCode.Should().Be(500);
        }

        private static ExceptionContext GenerateExceptionContext(Exception exception)
        {
            var actionContext = new ActionContext(
               Substitute.For<HttpContext>(),
               Substitute.For<Microsoft.AspNetCore.Routing.RouteData>(),
               Substitute.For<ActionDescriptor>());

            var exceptionContext = new ExceptionContext(actionContext, new List<IFilterMetadata>());
            exceptionContext.Exception = exception;

            return exceptionContext;
        }
    }
}
