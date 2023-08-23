using Domain.Exercise;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Ports.Outgoing
{
    public interface IExerciseRepository
    {
        IExercise GetExerciseById(int id);
        List<IMuscle> GetMusclesInExerciseById(int exerciseId);
        List<IExercise> GetExercisesForMuscle(int muscleId);
        List<IMuscle> GetMusclesByMusclegroupId(int musclegroupId);
        List<IMusclegroup> GetMuscleGroups();
        List<IExercise> GetExercisesByWorkoutId(int workoutId);
        List<IExerciseStats> GetExerciseStats(int exerciseId, int userId);
    }
}
