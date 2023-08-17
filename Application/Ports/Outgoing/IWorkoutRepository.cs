﻿using Domain.Workout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Ports.Outgoing
{
    public interface IWorkoutRepository
    {
        void SaveWorkout(IWorkout workout);
        IWorkout GenerateNewWorkout();
        List<IWorkout> GetWorkoutHistory(string userId);
        IWorkout StartWorkoutFromHistory(int userId, int workoutId);
    }
}
