using AutoMapper;
using FitnessTracker.Contracts.Request.Auth;
using FitnessTracker.Contracts.Request.Coach;
using FitnessTracker.Contracts.Request.Queries;
using FitnessTracker.Contracts.Request.User;
using FitnessTracker.Models;
using FitnessTracker.Models.Filters;

namespace FitnessTracker.MappingProfiles
{
    public class RequestToModelProfile : Profile
    {
        public RequestToModelProfile()
        {
            CreateMap<PaginationQuery, PaginationFilter>();

            CreateMap<AuthRegisterRequest, User>();
            CreateMap<AuthChangePasswordRequest, User>();

            CreateMap<UpdateUserRequest, User>();
            CreateMap<UpdateUserRoleRequest, User>();

            // Coach
            CreateMap<CreateCoach, Coach>();
            // /Coach

        }
    }
}