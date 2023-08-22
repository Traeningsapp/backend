﻿using Domain.Workout;

namespace Application.Ports.Incoming
{
    public interface IWorkoutUseCase
    {
        List<IWorkout> GenerateNewWorkout(int split_id);
        void SaveWorkout(int userId, string workoutAsJson);
        List<IWorkout> GetWorkoutHistory(int userId);
        IWorkout StartWorkoutFromHistory(int userId, int workoutId);
    }
}
