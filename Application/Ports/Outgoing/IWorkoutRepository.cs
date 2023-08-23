using Domain.Exercise;
using Domain.Workout;
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
        List<IExercise> GenerateExercisesForNewWorkout(int split_id);
        List<IWorkout> GetWorkoutHistoryByUserId(string userId);
        IWorkout StartWorkoutFromHistory(string userId, int workoutId);
    }
}
