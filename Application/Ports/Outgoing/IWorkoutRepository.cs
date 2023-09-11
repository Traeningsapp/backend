using Domain.Exercise;
using Domain.Workout;

namespace Application.Ports.Outgoing
{
    public interface IWorkoutRepository
    {
        int SaveWorkout(IWorkout workout);
        List<IExercise> GenerateExercisesForNewWorkout(int split_id);
        List<IWorkout> GetWorkoutHistoryByUserId(string userId);
        IWorkout StartWorkoutFromHistory(string userId, int workoutId);
        void DeleteWorkout(int workoutId);
    }
}
