// <copyright file="AutoMapping.cs" company="dagaren">
// Copyright (c) dagaren. All rights reserved.
// </copyright>

namespace Example.API.DataAccess
{
    using AutoMapper;

    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            this.CreateMap<Domain.Model.UserInfo, User>();
            this.CreateMap<Domain.Model.User, User>();
            this.CreateMap<User, Domain.Model.User>();
        }
    }
}