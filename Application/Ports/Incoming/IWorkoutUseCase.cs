﻿using Domain.Workout;

namespace Application.Ports.Incoming
{
    public interface IWorkoutUseCase
    {
        IWorkout GenerateNewWorkout();
        void SaveWorkout(int userId, string workoutAsJson);
        List<IWorkout> GetWorkoutHistory(int userId);
        IWorkout StartWorkoutFromHistory(int userId, int workoutId);
    }
}
