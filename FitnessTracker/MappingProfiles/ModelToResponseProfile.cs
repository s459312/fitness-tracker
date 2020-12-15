using AutoMapper;
using FitnessTracker.Contracts.Response.Exercise;
using FitnessTracker.Contracts.Response.Goal;
using FitnessTracker.Contracts.Response.Role;
using FitnessTracker.Contracts.Response.Training;
using FitnessTracker.Contracts.Response.User;
using FitnessTracker.Models;

namespace FitnessTracker.MappingProfiles
{
    public class ModelToResponseProfile : Profile
    {
        public ModelToResponseProfile()
        {
            CreateMap<Role, RoleResponse>();
            CreateMap<Goal, GoalResponse>();

            CreateMap<User, UserResponse>();
            CreateMap<User, UserMinifiedResponse>();

            CreateMap<Exercise, ExerciseResponse>();
            CreateMap<Exercise, ExerciseMinifiedResponse>()
                .ForMember(x => x.Goal,
                    x => x.MapFrom(
                        y => y.Goal.Name    
                    )
                );

            CreateMap<Training, TrainingFullResponse>();
            CreateMap<Training, TrainingMinifiedResponse>();
            CreateMap<Training, PublicTrainingResponse>();
            CreateMap<Exercise, TrainingExercise>();
        }
    }
}