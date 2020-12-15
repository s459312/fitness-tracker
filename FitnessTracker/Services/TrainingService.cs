using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.Internal;
using FitnessTracker.Contracts.Response.Exercise;
using FitnessTracker.Contracts.Response.Training;
using FitnessTracker.Data;
using FitnessTracker.Helpers;
using FitnessTracker.Models;
using FitnessTracker.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FitnessTracker.Services
{
    public class TrainingService : ITrainingService
    {
        private readonly DatabaseContext _context;
        private readonly IAuthHelper _authHelper;
        private readonly IMapper _mapper;

        public TrainingService(DatabaseContext context, IAuthHelper authHelper, IMapper mapper)
        {
            _context = context;
            _authHelper = authHelper;
            _mapper = mapper;
        }

        public async Task<List<TrainingMinifiedResponse>> GetAllUserTrainingsAsync()
        {
            return await _context.UserTraining
                .Where(x => x.UserId == _authHelper.GetAuthenticatedUserId())
                .Include(x => x.Training)
                .OrderByDescending(x => x.Favourite)
                .ThenBy(x => x.TrainingId)
                .Select(x => new TrainingMinifiedResponse
                {
                    Id = x.TrainingId,
                    Name = x.Training.Name,
                    Favourite = x.Favourite,
                    IsPublic = x.Training.IsPublic
                })
                .ToListAsync();
        }

        public async Task<List<PublicTrainingResponse>> GetAllAvailableUserPublicTrainingsAsync()
        {
            User user = await _context.Users.FirstOrDefaultAsync(x => x.Id == _authHelper.GetAuthenticatedUserId());
            var userAvailablePublicTrainings =
                from training in _context.Training
                where training.IsPublic == true &&
                      !(
                          from ut in user.UserTraining
                          select ut.TrainingId
                      ).Contains(training.Id)
                select new PublicTrainingResponse
                {
                    Id = training.Id,
                    Name = training.Name
                };
            return await userAvailablePublicTrainings.ToListAsync();
        }

        public async Task<List<Training>> GetAllPublicTrainingsAsync()
        {
            return await _context.Training
                .Where(x => x.IsPublic == true)
                .ToListAsync();
        }
        
        public async Task<TrainingFullResponse> GetFullTrainingByIdAsync(int trainingId)
        {
            var queryable = _context.UserTraining
                .Include(x => x.Training)
                .Where(x => x.UserId == _authHelper.GetAuthenticatedUserId() || x.Training.IsPublic)
                .AsQueryable();
            
            var training = await queryable.Select(x => new TrainingFullResponse
                {
                    Id  = x.TrainingId,
                    Description = x.Training.Description,
                    Name = x.Training.Name,
                    IsPublic = x.Training.IsPublic,
                    Favourite = x.Favourite
                })
                .FirstOrDefaultAsync(x => x.Id == trainingId);

            if (training != null)
            {
                var exercises = await _context.TrainingExercise
                    .Where(x => x.TrainingId == trainingId)
                    .Include(x => x.Exercise)
                    .ThenInclude(x => x.Goal)
                    .Select(x => x.Exercise)
                    .ToListAsync();

                training.TrainingExercises = _mapper.Map<List<ExerciseMinifiedResponse>>(exercises);
            }
            
            return training;
        }
        
        public async Task<Training> GetTrainingByIdAsync(int trainingId)
        {
            var queryable = _context.UserTraining
                .Include(x => x.Training)
                .AsQueryable();
            
            queryable = queryable.Where(x =>  x.UserId == _authHelper.GetAuthenticatedUserId());
            return await queryable.Select(x => x.Training)
                .FirstOrDefaultAsync(x => x.Id == trainingId);
        }

        public async Task<Training> GetPublicTrainingByIdAsync(int trainingId)
        {
            return await _context.Training.FirstOrDefaultAsync(x => x.IsPublic && x.Id == trainingId);
        }

        public async Task<Training> CreateTrainingAsync(Training training)
        {
            User user = await _context.Users.FirstOrDefaultAsync(x => x.Id == _authHelper.GetAuthenticatedUserId());

            UserTraining newUserTraining = new UserTraining
            {
                UserId = user.Id,
                Training = training
            };

            _context.Entry(user).Context
                .Add(newUserTraining);
            await _context.SaveChangesAsync();
            
            return newUserTraining.Training;
        }
        
        public async Task<Training> CreatePublicTrainingAsync(Training training)
        {
            training.IsPublic = true;
            await _context.Training.AddAsync(training);
            await _context.SaveChangesAsync();
            
            return training;
        }

        public async Task<bool> AddPublicTrainingToUser(Training training)
        {
            bool alreadyExists = await _context.UserTraining.AnyAsync(x =>
                x.TrainingId == training.Id && x.UserId == _authHelper.GetAuthenticatedUserId());

            if (alreadyExists)
                return true;
            
            await _context.UserTraining.AddAsync(new UserTraining
            {
                TrainingId = training.Id,
                UserId = _authHelper.GetAuthenticatedUserId()
            });
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> ToggleTrainingFavouriteAsync(Training training, bool favourite)
        {
            _context.UserTraining.Update(new UserTraining
            {
                TrainingId = training.Id, 
                UserId = _authHelper.GetAuthenticatedUserId(),
                Favourite = favourite
            });
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateTrainingAsync(Training training)
        {
            _context.Training.Update(training);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteTrainingAsync(Training training)
        {
            if (training.IsPublic)
                _context.UserTraining.Remove(new UserTraining
                {
                    TrainingId = training.Id,
                    UserId = _authHelper.GetAuthenticatedUserId()
                });
            else
                _context.Training.Remove(training);
            
            var deleted = await _context.SaveChangesAsync();
            return deleted > 0;
        }
            
        public async Task<bool> DeletePublicTrainingAsync(Training training)
        {
            _context.Training.Remove(training);
            var deleted = await _context.SaveChangesAsync();
            return deleted > 0;
        }

        public async Task<bool> UpdateTrainingExercisesAsync(Training training, int[] exerciseIds)
        {
            List<TrainingExercise> trainingExercises = new List<TrainingExercise>();
            foreach (int exerciseId in exerciseIds)
            {
                trainingExercises.Add(new TrainingExercise {ExerciseId = exerciseId, TrainingId = training.Id});
            }

            try
            {
                _context.TrainingExercise
                    .RemoveRange(_context.TrainingExercise.Where(x => x.TrainingId == training.Id));
                await _context.TrainingExercise.AddRangeAsync(trainingExercises);
                await _context.SaveChangesAsync();
            }
            catch
            {
                return false;
            }

            return true;
        }

        public async Task<bool> AddTrainingToHistory(Training training, List<ExerciseHistory> exerciseHistories)
        {
            int userId = _authHelper.GetAuthenticatedUserId();
            User user = await _context.Users
                .Include(x => x.ExerciseHistories)
                .FirstOrDefaultAsync(x => x.Id == userId);
            
            user.TrainingHistories.Add(new TrainingHistory { Training = training, Date = DateTime.Now });
            
            
            foreach (ExerciseHistory exerciseHistory in exerciseHistories)
            {
                var alreadyExists = user.ExerciseHistories
                    .FirstOrDefault(x => 
                        x.ExerciseId == exerciseHistory.ExerciseId && 
                        x.Date == DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd"))
                    );
                
                if (alreadyExists != null)
                {
                    var stats = exerciseHistory.ExerciseHistoryStats;
                    stats.ForAll(x => x.ExerciseHistoryId = alreadyExists.Id);
                    await _context.ExerciseHistoryStats.AddRangeAsync(stats);
                }
                else
                {
                    user.ExerciseHistories.Add(new ExerciseHistory
                    {
                        UserId = userId,
                        ExerciseId = exerciseHistory.ExerciseId,
                        Date = DateTime.Now,
                        ExerciseHistoryStats = exerciseHistory.ExerciseHistoryStats
                    });
                }
            }

            return await _context.SaveChangesAsync() > 0;
        }
    }
}