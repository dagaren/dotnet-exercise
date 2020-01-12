// <copyright file="AutoMapping.cs" company="dagaren">
// Copyright (c) dagaren. All rights reserved.
// </copyright>

namespace Example.API.Controllers
{
    using AutoMapper;
    using Example.API.Controllers.ViewModels;
    using Example.API.Domain.Model;

    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            this.CreateMap<UserRequest, UserInfo>();
            this.CreateMap<UserRequest, User>();
            this.CreateMap<User, UserResponse>()
                    .ForMember(user => user.Birthdate, m => m.MapFrom(userResponse => userResponse.Birthdate.ToString("yyyy-MM-dd")));
        }
    }
}
