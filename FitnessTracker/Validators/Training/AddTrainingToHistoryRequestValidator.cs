using System.Collections.Generic;
using System.Linq;
using FitnessTracker.Contracts.Request.Training;
using FitnessTracker.Services.Interfaces;
using FluentValidation;

namespace FitnessTracker.Validators.Training
{
    public class AddTrainingToHistoryRequestValidator : AbstractValidator<AddTrainingToHistoryRequest>
    {
        public AddTrainingToHistoryRequestValidator(IExerciseService exerciseService)
        {
            RuleFor(x => x)
                .Custom((list, context) =>
                {
                    List<int> exerciseIds = new List<int>();
                    foreach (ExerciseHistoryRequest exerciseHistoryRequest in list.Exercises)
                    {
                        exerciseIds.Add(exerciseHistoryRequest.ExerciseId);
                    }

                    if (!exerciseService.AllExercisesBelongsToTraining(list.TrainingId, exerciseIds.ToArray()))
                    {
                        context.AddFailure("Jedno lub więcej ćwiczeń nie należy do treningu");
                    }

                    if (!exerciseService.AllExercisesExists(exerciseIds.ToArray()))
                    {
                        context.AddFailure("Jedno z podanych ćwiczeń jest błędne");
                    }
                });
                
            RuleForEach(x => x.Exercises)
                .ChildRules(exercise =>
                {
                    exercise.RuleFor(x => x.ExerciseId)
                        .GreaterThan(0).WithMessage("Podano złe ćwiczenie")
                        .DependentRules(() =>
                        {
                            exercise.RuleFor(x => x.ExerciseHistoryStat)
                                .Custom((request, context) =>
                                {
                                    if (request.Czas <= 0 && request.Dystans <= 0 && request.Obciazenie <= 0 && request.Powtorzenia <= 0 && request.Serie <= 0)
                                        context.AddFailure("Błędne prametry ćwiczenia");
                                    if (request.Obciazenie > 0)
                                        if (request.Czas > 0 || request.Dystans > 0)
                                            context.AddFailure("Błędne prametry ćwiczenia");
                                    if (request.Powtorzenia > 0)
                                        if (request.Czas > 0 || request.Dystans > 0)
                                            context.AddFailure("Błędne prametry ćwiczenia");
                                    if (request.Serie > 0)
                                        if (request.Czas > 0 || request.Dystans > 0)
                                            context.AddFailure("Błędne prametry ćwiczenia");
                                    if (request.Czas > 0)
                                        if (request.Powtorzenia > 0 || request.Serie > 0 || request.Obciazenie > 0)
                                            context.AddFailure("Błędne prametry ćwiczenia");
                                    if (request.Dystans > 0)
                                        if (request.Powtorzenia > 0 || request.Serie > 0 || request.Obciazenie > 0)
                                            context.AddFailure("Błędne prametry ćwiczenia");
                                });
                        });
                });
        }

    }
}