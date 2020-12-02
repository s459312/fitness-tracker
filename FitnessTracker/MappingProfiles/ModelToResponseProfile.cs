using AutoMapper;
using FitnessTracker.Contracts.Response.Role;
using FitnessTracker.Contracts.Response.User;
using FitnessTracker.Models;

namespace FitnessTracker.MappingProfiles
{
    public class ModelToResponseProfile : Profile
    {
        public ModelToResponseProfile()
        {
            CreateMap<Role, RoleResponse>();
            
            CreateMap<User, UserResponse>();
            CreateMap<User, UserMinifiedResponse>();
        }
    }
}